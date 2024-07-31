using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class CaidaImageController : MonoBehaviour
{
    public RawImage caidaPanel; // Referencia a la RawImage
    public Button caidaButton; // Referencia al botón que controla la RawImage
    private bool isCaidaImageVisible = false; // Estado de visibilidad de la RawImage
    private bool isCaidaImageLoading = false; // Estado de carga de la imagen
    private string caidaImageUrl = "http://localhost:3000/images/Caida.png"; // URL de la imagen

    void Start()
    {
        // Asegurarse de que el botón tiene el listener asignado
        if (caidaButton != null)
        {
            caidaButton.onClick.AddListener(ToggleCaidaImage);
            Debug.Log("Listener asignado al botón.");
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }

        // Asegurarse de que la RawImage esté inicialmente oculta
        if (caidaPanel != null)
        {
            caidaPanel.gameObject.SetActive(false);
            Debug.Log("La RawImage está inicialmente oculta.");
        }
        else
        {
            Debug.LogError("La RawImage no está asignada en el Inspector.");
        }
    }

    public void ToggleCaidaImage()
    {
        if (isCaidaImageLoading) return; // Prevenir múltiples solicitudes simultáneas

        if (isCaidaImageVisible)
        {
            HideCaidaImage();
        }
        else
        {
            caidaPanel.gameObject.SetActive(true); // Activar temporalmente para iniciar la coroutine
            StartCoroutine(LoadCaidaImageFromAPI());
        }
    }

    private void HideCaidaImage()
    {
        caidaPanel.texture = null; // Liberar la textura anterior
        caidaPanel.gameObject.SetActive(false);
        isCaidaImageVisible = false;
        isCaidaImageLoading = false;
        Debug.Log("Imagen oculta.");
    }

    IEnumerator LoadCaidaImageFromAPI()
    {
        isCaidaImageLoading = true;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(caidaImageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading image: " + request.error);
            HideCaidaImage();
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (caidaPanel != null)
            {
                caidaPanel.texture = texture;
                isCaidaImageVisible = true;
                Debug.Log("Imagen cargada y asignada.");
            }
            else
            {
                Debug.LogError("La RawImage no está asignada en el Inspector.");
                caidaPanel.gameObject.SetActive(false); // Desactivar si no está asignada
            }
        }

        isCaidaImageLoading = false;

        // Asegurarse de que la imagen se oculta si no se cargó correctamente
        if (!isCaidaImageVisible)
        {
            caidaPanel.gameObject.SetActive(false);
        }
    }
}

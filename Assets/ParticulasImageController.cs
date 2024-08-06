using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ParticulasImageController : MonoBehaviour
{
    public RawImage particulasPanel; // Referencia a la RawImage
    public Button particulasBtn; // Referencia al botón que controla la RawImage
    private bool isParticulasImageVisible = false; // Estado de visibilidad de la RawImage
    private bool isParticulasImageLoading = false; // Estado de carga de la imagen
    private string particulasImageUrl = "http://localhost:3000/images/rt.png"; // URL de la imagen

    void Start()
    {
        // Asegurarse de que el botón tiene el listener asignado
        if (particulasBtn != null)
        {
            particulasBtn.onClick.AddListener(ToggleParticulasImage);
            Debug.Log("Listener asignado al botón.");
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }

        // Asegurarse de que la RawImage esté inicialmente oculta
        if (particulasPanel != null)
        {
            particulasPanel.gameObject.SetActive(false);
            Debug.Log("La RawImage está inicialmente oculta.");
        }
        else
        {
            Debug.LogError("La RawImage no está asignada en el Inspector.");
        }
    }

    public void ToggleParticulasImage()
    {
        if (isParticulasImageLoading) return; // Prevenir múltiples solicitudes simultáneas

        if (isParticulasImageVisible)
        {
            HideParticulasImage();
        }
        else
        {
            particulasPanel.gameObject.SetActive(true); // Activar temporalmente para iniciar la coroutine
            StartCoroutine(LoadParticulasImageFromAPI());
        }
    }

    private void HideParticulasImage()
    {
        particulasPanel.texture = null; // Liberar la textura anterior
        particulasPanel.gameObject.SetActive(false);
        isParticulasImageVisible = false;
        isParticulasImageLoading = false;
        Debug.Log("Imagen oculta.");
    }

    IEnumerator LoadParticulasImageFromAPI()
    {
        isParticulasImageLoading = true;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(particulasImageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading image: " + request.error);
            HideParticulasImage();
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (particulasPanel != null)
            {
                particulasPanel.texture = texture;
                isParticulasImageVisible = true;
                Debug.Log("Imagen cargada y asignada.");
            }
            else
            {
                Debug.LogError("La RawImage no está asignada en el Inspector.");
                particulasPanel.gameObject.SetActive(false); // Desactivar si no está asignada
            }
        }

        isParticulasImageLoading = false;

        // Asegurarse de que la imagen se oculta si no se cargó correctamente
        if (!isParticulasImageVisible)
        {
            particulasPanel.gameObject.SetActive(false);
        }
    }
}

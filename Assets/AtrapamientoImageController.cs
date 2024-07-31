using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class AtrapamientoImageController : MonoBehaviour
{
    public RawImage panelAtrapamiento; // Referencia a la RawImage
    public Button botonAtrapamiento; // Referencia al botón que controla la RawImage
    private bool isAtrapamientoImageVisible = false; // Estado de visibilidad de la RawImage
    private bool isAtrapamientoImageLoading = false; // Estado de carga de la imagen
    private string atrapamientoImageUrl = "http://localhost:3000/images/Atrapamiento.png"; // URL de la imagen

    void Start()
    {
        // Asegurarse de que el botón tiene el listener asignado
        if (botonAtrapamiento != null)
        {
            botonAtrapamiento.onClick.AddListener(ToggleAtrapamientoImage);
            Debug.Log("Listener asignado al botón.");
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }

        // Asegurarse de que la RawImage esté inicialmente oculta
        if (panelAtrapamiento != null)
        {
            panelAtrapamiento.gameObject.SetActive(false);
            Debug.Log("La RawImage está inicialmente oculta.");
        }
        else
        {
            Debug.LogError("La RawImage no está asignada en el Inspector.");
        }
    }

    public void ToggleAtrapamientoImage()
    {
        if (isAtrapamientoImageLoading) return; // Prevenir múltiples solicitudes simultáneas

        if (isAtrapamientoImageVisible)
        {
            HideAtrapamientoImage();
        }
        else
        {
            panelAtrapamiento.gameObject.SetActive(true); // Activar temporalmente para iniciar la coroutine
            StartCoroutine(LoadAtrapamientoImageFromAPI());
        }
    }

    private void HideAtrapamientoImage()
    {
        panelAtrapamiento.texture = null; // Liberar la textura anterior
        panelAtrapamiento.gameObject.SetActive(false);
        isAtrapamientoImageVisible = false;
        isAtrapamientoImageLoading = false;
        Debug.Log("Imagen oculta.");
    }

    IEnumerator LoadAtrapamientoImageFromAPI()
    {
        isAtrapamientoImageLoading = true;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(atrapamientoImageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading image: " + request.error);
            HideAtrapamientoImage();
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (panelAtrapamiento != null)
            {
                panelAtrapamiento.texture = texture;
                isAtrapamientoImageVisible = true;
                Debug.Log("Imagen cargada y asignada.");
            }
            else
            {
                Debug.LogError("La RawImage no está asignada en el Inspector.");
                panelAtrapamiento.gameObject.SetActive(false); // Desactivar si no está asignada
            }
        }

        isAtrapamientoImageLoading = false;

        // Asegurarse de que la imagen se oculta si no se cargó correctamente
        if (!isAtrapamientoImageVisible)
        {
            panelAtrapamiento.gameObject.SetActive(false);
        }
    }
}

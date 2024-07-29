using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ImageController : MonoBehaviour
{
    public RawImage panel; // Referencia a la RawImage
    public Button button; // Referencia al botón que controla la RawImage
    private bool isImageVisible = false; // Estado de visibilidad de la RawImage
    private bool isImageLoading = false; // Estado de carga de la imagen
    private string imageUrl = "http://localhost:3000/images/brazo.jpg"; // URL de la imagen

    public void Start()
    {
        // Asegurarse de que el botón tiene el listener asignado
        if (button != null)
        {
            button.onClick.AddListener(ToggleImage);
            Debug.Log("Listener asignado al botón.");
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }

        // Asegurarse de que la RawImage esté inicialmente oculta
        if (panel != null)
        {
            panel.gameObject.SetActive(false);
            Debug.Log("La RawImage está inicialmente oculta.");
        }
        else
        {
            Debug.LogError("La RawImage no está asignada en el Inspector.");
        }
    }

    public void ToggleImage()
    {
        if (isImageLoading) return; // Prevenir múltiples solicitudes simultáneas

        if (isImageVisible)
        {
            HideImage();
        }
        else
        {
            panel.gameObject.SetActive(true); // Activar temporalmente para iniciar la coroutine
            StartCoroutine(LoadImageFromAPI());
        }
    }

    private void HideImage()
    {
        panel.texture = null; // Liberar la textura anterior
        panel.gameObject.SetActive(false);
        isImageVisible = false;
        isImageLoading = false;
        Debug.Log("Imagen oculta.");
    }

    IEnumerator LoadImageFromAPI()
    {
        isImageLoading = true;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading image: " + request.error);
            HideImage();
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (panel != null)
            {
                panel.texture = texture;
                isImageVisible = true;
                Debug.Log("Imagen cargada y asignada.");
            }
            else
            {
                Debug.LogError("La RawImage no está asignada en el Inspector.");
                panel.gameObject.SetActive(false); // Desactivar si no está asignada
            }
        }

        isImageLoading = false;

        // Asegurarse de que la imagen se oculta si no se cargó correctamente
        if (!isImageVisible)
        {
            panel.gameObject.SetActive(false);
        }
    }
}


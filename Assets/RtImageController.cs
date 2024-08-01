using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class RtImageController : MonoBehaviour
{
    public RawImage panelRt; // Referencia a la RawImage
    public Button btnRt; // Referencia al botón que controla la RawImage
    private bool isRtImageVisible = false; // Estado de visibilidad de la RawImage
    private bool isRtImageLoading = false; // Estado de carga de la imagen
    private string rtImageUrl = "http://localhost:3000/images/rt.png"; // URL de la imagen

    void Start()
    {
        // Asegurarse de que el botón tiene el listener asignado
        if (btnRt != null)
        {
            btnRt.onClick.AddListener(ToggleRtImage);
            Debug.Log("Listener asignado al botón.");
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }

        // Asegurarse de que la RawImage esté inicialmente oculta
        if (panelRt != null)
        {
            panelRt.gameObject.SetActive(false);
            Debug.Log("La RawImage está inicialmente oculta.");
        }
        else
        {
            Debug.LogError("La RawImage no está asignada en el Inspector.");
        }
    }

    public void ToggleRtImage()
    {
        if (isRtImageLoading) return; // Prevenir múltiples solicitudes simultáneas

        if (isRtImageVisible)
        {
            HideRtImage();
        }
        else
        {
            panelRt.gameObject.SetActive(true); // Activar temporalmente para iniciar la coroutine
            StartCoroutine(LoadRtImageFromAPI());
        }
    }

    private void HideRtImage()
    {
        panelRt.texture = null; // Liberar la textura anterior
        panelRt.gameObject.SetActive(false);
        isRtImageVisible = false;
        isRtImageLoading = false;
        Debug.Log("Imagen oculta.");
    }

    IEnumerator LoadRtImageFromAPI()
    {
        isRtImageLoading = true;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(rtImageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading image: " + request.error);
            HideRtImage();
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (panelRt != null)
            {
                panelRt.texture = texture;
                isRtImageVisible = true;
                Debug.Log("Imagen cargada y asignada.");
            }
            else
            {
                Debug.LogError("La RawImage no está asignada en el Inspector.");
                panelRt.gameObject.SetActive(false); // Desactivar si no está asignada
            }
        }

        isRtImageLoading = false;

        // Asegurarse de que la imagen se oculta si no se cargó correctamente
        if (!isRtImageVisible)
        {
            panelRt.gameObject.SetActive(false);
        }
    }
}

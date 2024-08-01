using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class DespegueImageController : MonoBehaviour
{
    public RawImage panelDespegue; // Referencia a la RawImage
    public Button btndespegue; // Referencia al botón que controla la RawImage
    private bool isDespegueImageVisible = false; // Estado de visibilidad de la RawImage
    private bool isDespegueImageLoading = false; // Estado de carga de la imagen
    private string despegueImageUrl = "http://localhost:3000/images/Despegue.png"; // URL de la imagen

    void Start()
    {
        // Asegurarse de que el botón tiene el listener asignado
        if (btndespegue != null)
        {
            btndespegue.onClick.AddListener(ToggleDespegueImage);
            Debug.Log("Listener asignado al botón.");
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }

        // Asegurarse de que la RawImage esté inicialmente oculta
        if (panelDespegue != null)
        {
            panelDespegue.gameObject.SetActive(false);
            Debug.Log("La RawImage está inicialmente oculta.");
        }
        else
        {
            Debug.LogError("La RawImage no está asignada en el Inspector.");
        }
    }

    public void ToggleDespegueImage()
    {
        if (isDespegueImageLoading) return; // Prevenir múltiples solicitudes simultáneas

        if (isDespegueImageVisible)
        {
            HideDespegueImage();
        }
        else
        {
            panelDespegue.gameObject.SetActive(true); // Activar temporalmente para iniciar la coroutine
            StartCoroutine(LoadDespegueImageFromAPI());
        }
    }

    private void HideDespegueImage()
    {
        panelDespegue.texture = null; // Liberar la textura anterior
        panelDespegue.gameObject.SetActive(false);
        isDespegueImageVisible = false;
        isDespegueImageLoading = false;
        Debug.Log("Imagen oculta.");
    }

    IEnumerator LoadDespegueImageFromAPI()
    {
        isDespegueImageLoading = true;

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(despegueImageUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading image: " + request.error);
            HideDespegueImage();
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (panelDespegue != null)
            {
                panelDespegue.texture = texture;
                isDespegueImageVisible = true;
                Debug.Log("Imagen cargada y asignada.");
            }
            else
            {
                Debug.LogError("La RawImage no está asignada en el Inspector.");
                panelDespegue.gameObject.SetActive(false); // Desactivar si no está asignada
            }
        }

        isDespegueImageLoading = false;

        // Asegurarse de que la imagen se oculta si no se cargó correctamente
        if (!isDespegueImageVisible)
        {
            panelDespegue.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // Referencia al panel del pop-up
    public RawImage popupImage; // Referencia a la imagen dentro del panel
    public Button toggleButton; // Referencia al botón que controla el pop-up
    private bool isPopupVisible = false; // Estado de visibilidad del pop-up
    private string imageUrl = "http://localhost:3000/images/brazo.jpg"; // URL de la imagen

    public void Start()
    {
        // Asegurarse de que el botón tiene el listener asignado
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(TogglePopup);
            Debug.Log("Listener asignado al botón.");
        }
        else
        {
            Debug.LogError("El botón no está asignado en el Inspector.");
        }

        // Asegurarse de que el panel del pop-up esté inicialmente oculto
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
            Debug.Log("El panel del pop-up está inicialmente oculto.");
        }
        else
        {
            Debug.LogError("El panel del pop-up no está asignado en el Inspector.");
        }
    }

    public void TogglePopup()
    {
        // Alternar el estado de visibilidad del pop-up
        isPopupVisible = !isPopupVisible;
        if (popupPanel != null)
        {
            popupPanel.SetActive(isPopupVisible);
            Debug.Log("Pop-up visible: " + isPopupVisible);
            Debug.Log("Estado del panel del pop-up: " + popupPanel.activeSelf);
            Debug.Log("Posición del panel del pop-up: " + popupPanel.transform.position);
            Debug.Log("Tamaño del panel del pop-up: " + popupPanel.GetComponent<RectTransform>().rect.size);
            Debug.Log("Estado de la imagen del pop-up: " + popupImage.gameObject.activeSelf);
        }
        else
        {
            Debug.LogError("El panel del pop-up no está asignado en el Inspector.");
        }

        // Si el pop-up se está mostrando, cargar la imagen desde la API
        if (isPopupVisible)
        {
            StartCoroutine(LoadImageFromAPI());
        }
    }

    IEnumerator LoadImageFromAPI()
    {
        // Solicitud para obtener la textura desde la URL
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        // Manejo de errores en caso de que la solicitud falle
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading image: " + request.error);
        }
        else
        {
            // Asignar la textura descargada a la RawImage del pop-up
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (popupImage != null)
            {
                popupImage.texture = texture;
                Debug.Log("Imagen cargada y asignada.");
                Debug.Log("Color de la imagen del pop-up: " + popupImage.color);
                Debug.Log("Posición de la imagen del pop-up: " + popupImage.transform.position);
                Debug.Log("Tamaño de la imagen del pop-up: " + popupImage.GetComponent<RectTransform>().rect.size);
            }
            else
            {
                Debug.LogError("La imagen del pop-up no está asignada en el Inspector.");
            }
        }
    }
}

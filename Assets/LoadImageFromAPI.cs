using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LoadImageFromAPI : MonoBehaviour
{
    public string apiBaseUrl = "localhost:3000/images/"; // URL base de la API
    public RawImage rawImage;

    void Start()
    {
        // Obtener el nombre del objeto RawImage en Unity
        string imageName = rawImage.name;

        // Construir la URL completa utilizando el nombre del objeto
        string imageUrl = apiBaseUrl + imageName + ".png";

        // Iniciar la descarga de la imagen
        StartCoroutine(DownloadImage(imageUrl));

        Debug.Log(imageUrl);
    }

    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            rawImage.texture = texture;
            Debug.Log("Sexo obtenido al descargar"+ url);
        }
        else
        {
            Debug.LogError("Error al descargar la imagen: " + request.error);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    public RawImage rawImage; // Arrastra el RawImage aquí desde el Inspector
    private string imageUrl = "http://localhost:3000/images/Colisi%C3%83%C2%B3n%20equipos%20MHE.png";

    public int maxWidth = 5; // Ancho máximo para el RawImage
    public int maxHeight = 5; // Alto máximo para el RawImage

    void Start()
    {
        StartCoroutine(LoadImageFromURL(imageUrl));
    }

    IEnumerator LoadImageFromURL(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            // Calcula el tamaño adecuado para la imagen
            float aspectRatio = (float)texture.width / texture.height;
            int newWidth = texture.width;
            int newHeight = texture.height;

            // Ajusta el tamaño si es necesario
            if (texture.width > maxWidth || texture.height > maxHeight)
            {
                if (texture.width > texture.height)
                {
                    newWidth = maxWidth;
                    newHeight = Mathf.RoundToInt(newWidth / aspectRatio);
                }
                else
                {
                    newHeight = maxHeight;
                    newWidth = Mathf.RoundToInt(newHeight * aspectRatio);
                }

                // Verifica que los valores sean válidos
                newWidth = Mathf.Max(newWidth, 1);
                newHeight = Mathf.Max(newHeight, 1);
            }

            // Ajusta el tamaño del RawImage
            rawImage.texture = texture;
            rawImage.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

            // Inicia el efecto de latido
            StartCoroutine(PulseEffect());
        }
        else
        {
            Debug.LogError($"Error downloading image: {request.error}");
        }
    }

    IEnumerator PulseEffect()
    {
        while (true)
        {
            // Efecto de agrandar
            for (float i = 1f; i <= 1.1f; i += 0.01f)
            {
                rawImage.rectTransform.localScale = new Vector3(i, i, 1f);
                yield return new WaitForSeconds(0.05f);
            }

            // Efecto de encoger
            for (float i = 1.1f; i >= 1f; i -= 0.01f)
            {
                rawImage.rectTransform.localScale = new Vector3(i, i, 1f);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Asignar la URL del video
        string videoName = videoPlayer.name;

        //Link Produccion
        //videoPlayer.url = "https://backendapi-84rm.onrender.com/videos/" + videoName + ".mp4";

        //Link Desarrollo

        videoPlayer.url = "http://localhost:3000/videos/" + videoName + ".mp4";

        // Intentar preparar el video y manejar cualquier excepción
        try
        {
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += OnVideoPrepared;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al intentar preparar el video: " + e.Message);
            // Puedes manejar la excepción aquí, por ejemplo mostrando un mensaje de error en la UI
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        try
        {
            // Comenzar a reproducir el video una vez que está preparado
            videoPlayer.Play();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al intentar reproducir el video: " + e.Message);
            // Puedes manejar la excepción aquí, por ejemplo mostrando un mensaje de error en la UI
        }
    }
}


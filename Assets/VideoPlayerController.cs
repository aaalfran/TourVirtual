using UnityEngine;
using UnityEngine.Video;
using System.Collections;
using System.Net;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Configurar el VideoPlayer para que use VideoClip como fuente inicialmente
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = null;  // No asignar ningún videoclip al inicio

        // Desactivar la reproducción automática
        videoPlayer.playOnAwake = false;

        // Asignar la URL del video
        string videoName = videoPlayer.name;

        //Link Producción
        //string videoUrl = "https://backendapi-84rm.onrender.com/videos/" + videoName + ".mp4";

        //Link Desarrollo
        string videoUrl = "http://localhost:3000/videos/" + videoName + ".mp4";

        StartCoroutine(CheckAndLoadVideo(videoUrl));
    }

    IEnumerator CheckAndLoadVideo(string videoUrl)
    {
        // Verificar si el servidor está disponible antes de intentar cargar el video
        if (IsServerAvailable(videoUrl))
        {
            try
            {
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = videoUrl;

                // Preparar el video, pero no reproducirlo automáticamente
                videoPlayer.Prepare();
                videoPlayer.prepareCompleted += OnVideoPrepared;
                videoPlayer.errorReceived += OnVideoError;
            }
            catch (System.Exception)
            {
                Debug.Log("Error al intentar preparar el video");
                HandleVideoError();
            }
        }
        else
        {
            Debug.Log("El servidor no está disponible. Dejando el VideoPlayer vacío.");
            HandleVideoError();
        }

        yield return null;
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        Debug.Log("Video preparado. Listo para reproducir cuando el usuario lo desee.");
        // Aquí, el video está listo, pero no se reproduce automáticamente.
    }

    private void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.Log("Error recibido desde el VideoPlayer: " + message);
        HandleVideoError();
    }

    private void HandleVideoError()
    {
        videoPlayer.Stop();
        videoPlayer.url = null;
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = null;
    }

    private bool IsServerAvailable(string url)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                return response.StatusCode == HttpStatusCode.OK;
            }
        }
        catch
        {
            return false;
        }
    }

    // Método para que el usuario inicie la reproducción manualmente
    public void PlayVideo()
    {
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
    }
}





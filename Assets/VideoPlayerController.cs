using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Asignar la URL del video
        videoPlayer.url = "http://localhost:3000/videos/teflon.mp4";
        
        // Configurar el VideoPlayer para reproducir el video
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        // Comenzar a reproducir el video una vez que está preparado
        videoPlayer.Play();
    }
}

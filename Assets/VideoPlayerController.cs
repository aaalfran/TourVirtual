using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Asignar la URL del video
        string videoName = videoPlayer.name;
        videoPlayer.url = "https://backendapi-84rm.onrender.com/videos/" + videoName+".mp4";
        
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

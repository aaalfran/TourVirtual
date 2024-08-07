using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoMaquinaria2 : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button playButton;
    public Button pauseButton;
    public Button restartButton;

    private string videoUrl = "http://localhost:3000/videos/teflon.mp4";

    void Start()
    {
        videoPlayer.playOnAwake = false; // Asegúrate de que el video no se reproduzca automáticamente
        videoPlayer.url = videoUrl;
        
        // Suscribir los métodos a los eventos de los botones
        playButton.onClick.AddListener(PlayVideo);
        pauseButton.onClick.AddListener(PauseVideo);
        restartButton.onClick.AddListener(RestartVideo);
    }

    void PlayVideo()
    {
        videoPlayer.Play();
    }

    void PauseVideo()
    {
        videoPlayer.Pause();
    }

    void RestartVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Play();
    }
}

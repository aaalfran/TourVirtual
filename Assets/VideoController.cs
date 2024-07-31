using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button playButton;
    public Button pauseButton;
    public Button restartButton;

    private string videoUrl = "http://localhost:3000/videos/teflon.mp4";

    void Start()
    {
         videoPlayer.playOnAwake = false;
        videoPlayer.url = videoUrl;
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

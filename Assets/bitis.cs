using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayAndExit : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video bileşeni

    void Start()
    {
        videoPlayer.Play(); // Videoyu başlat
        videoPlayer.loopPointReached += OnVideoEnd; // Video bitince çıkış yap
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("UIScene");
    }
}
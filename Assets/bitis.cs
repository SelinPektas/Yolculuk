using UnityEngine;
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
        Application.Quit(); // Uygulamayı kapat
    }
}
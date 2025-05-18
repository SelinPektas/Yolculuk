using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class introScript : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video bileþeni

    void Start()
    {
        videoPlayer.Play(); // Videoyu baþlat
        videoPlayer.loopPointReached += OnVideoEnd; // Video bitince çýkýþ yap
        videoPlayer.SetDirectAudioMute(0, true);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene"); 
    }
}
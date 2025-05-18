using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class introScript : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video bile�eni

    void Start()
    {
        videoPlayer.Play(); // Videoyu ba�lat
        videoPlayer.loopPointReached += OnVideoEnd; // Video bitince ��k�� yap
        videoPlayer.SetDirectAudioMute(0, true);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene"); 
    }
}
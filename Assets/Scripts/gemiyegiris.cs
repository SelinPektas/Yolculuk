using UnityEngine;

public class gemiyegiris : MonoBehaviour
{
    public GameObject promptTextPlayer;   // Player için text
    public GameObject promptTextPlayer2;  // Player2 için text
    public GameObject enabledObj;
    public GameObject disabledObj1;
    public GameObject disabledObj2;
    public GameObject disabledObj3;

    private bool isPlayerNear = false;
    private bool isPlayer2Near = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (enabledObj != null)
                enabledObj.SetActive(true);
            if (disabledObj1 != null)
                disabledObj1.SetActive(false);
            if (disabledObj2 != null)
                disabledObj2.SetActive(false);
            if (disabledObj3 != null)
                disabledObj3.SetActive(false);
            if (promptTextPlayer != null)
                promptTextPlayer.SetActive(false);
            Destroy(gameObject); // Bu objeyi yok et
        }
        if (isPlayer2Near && Input.GetKeyDown(KeyCode.E))
        {
            if (enabledObj != null)
                enabledObj.SetActive(true);
            if (disabledObj1 != null)
                disabledObj1.SetActive(false);
            if (disabledObj2 != null)
                disabledObj2.SetActive(false);
            if (disabledObj3 != null)
                disabledObj3.SetActive(false);
            if (promptTextPlayer2 != null)
                promptTextPlayer2.SetActive(false);
            Destroy(gameObject); // Bu objeyi yok et
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (promptTextPlayer != null)
                promptTextPlayer.SetActive(true);
            isPlayerNear = true;
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            if (promptTextPlayer2 != null)
                promptTextPlayer2.SetActive(true);
            isPlayer2Near = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (promptTextPlayer != null)
                promptTextPlayer.SetActive(false);
            isPlayerNear = false;
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            if (promptTextPlayer2 != null)
                promptTextPlayer2.SetActive(false);
            isPlayer2Near = false;
        }
    }
}
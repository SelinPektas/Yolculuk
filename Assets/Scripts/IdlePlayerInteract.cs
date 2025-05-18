using UnityEngine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class IdlePlayerInteract : MonoBehaviour
{// Oynatılacak animasyon

    private GameObject player2Objesi; // Sahnedeki "çocuk" objesi
    private GameObject promptText;    // player2Objesi'nin "textçocuk" adlı child objesi
    private bool isPlayer2Near = false;

    void Start()
    {
        // Sahnedeki adı "çocuk" olan objeyi bul
        player2Objesi = GameObject.Find("çocuk");
        if (player2Objesi != null)
        {
            var textChild = player2Objesi.transform.Find("textçocuk");
            if (textChild != null)
            {
                promptText = textChild.gameObject;
                promptText.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isPlayer2Near)
        {
            Inventory inv = FindObjectOfType<Inventory>();
            bool hasFlower = inv != null && inv.items.Contains("Çiçek"); // items listesinde kontrol

            if (promptText != null)
                promptText.SetActive(hasFlower);

            if (hasFlower && Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("final"); // Buraya geçmek istediğin sahnenin adını yaz
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            isPlayer2Near = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            if (promptText != null)
                promptText.SetActive(false);
            isPlayer2Near = false;
        }
    }
}
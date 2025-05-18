using UnityEngine;
using Spine.Unity;

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
                var skeleton1 = GetComponent<SkeletonAnimation>();
                if (skeleton1 != null)
                    skeleton1.AnimationState.SetAnimation(0, "Row", false);
                // Player2'nin animasyonunu oynat
                if (player2Objesi != null)
                {
                    var skeleton2 = player2Objesi.GetComponent<SkeletonAnimation>();
                    if (skeleton2 != null)
                        skeleton2.AnimationState.SetAnimation(0, "NoDoorIdle", false);

                    // PlayerMovement2 scriptini devre dışı bırak
                    var movementScript = player2Objesi.GetComponent<PlayerMovement2>();
                    if (movementScript != null)
                        movementScript.enabled = false;
                }

                // ENVANTERDEN "Çiçek" EŞYASINI SİL
                if (inv != null)
                    inv.RemoveItem("Çiçek");

                if (promptText != null)
                    promptText.SetActive(false);

                enabled = false;
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
using UnityEngine;
using UnityEngine.UI;

public class Kalem : MonoBehaviour
{
    public GameObject baloncukUI; // Baloncuk objesi
    public GameObject promptText; // "E'ye bas" gibi uyarı texti
    public Sprite kalemKagitSprite; // Inspector'dan ata
    private bool isPlayerNear = false;

    void Update()
    {
        Inventory inv = FindObjectOfType<Inventory>();
        bool hasKalem = inv != null && inv.items.Contains("Kalem");

        if (isPlayerNear)
        {
            promptText.SetActive(true);
            baloncukUI.SetActive(true);

            if (hasKalem && Input.GetKeyDown(KeyCode.E))
            {
                promptText.SetActive(false);
                baloncukUI.SetActive(false);

                // Kalemi envanterden çıkar
                inv.AddItem("Kalem Kağıt", kalemKagitSprite);
                inv.RemoveItem("Kalem");

                // Takipçi başlat
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                GameObject activePlayer = null;
                GameObject otherPlayer = null;

                foreach (var player in players)
                {
                    var movement = player.GetComponent<PlayerMovement>();
                    if (movement != null && movement.enabled)
                        activePlayer = player;
                    else
                        otherPlayer = player;
                }

                if (otherPlayer != null)
                {
                    var follower = otherPlayer.GetComponent<Follower>();
                    if (follower != null)
                    {
                        follower.target = activePlayer.transform;
                        follower.enabled = true;
                    }
                }

                // PlayerSwitcher'ı aktif et
                PlayerSwitcher switcher = FindObjectOfType<PlayerSwitcher>();
                if (switcher != null)
                    switcher.enabled = true;

                // Eski kalem slotunu kaldır
                foreach (Transform slot in inv.inventoryPanel)
                {
                    Image img = slot.GetComponentInChildren<Image>();
                    if (img != null && img.sprite.name == "kalemSpriteAdı") // Sprite adını doğru gir
                    {
                        Destroy(slot.gameObject);
                        break;
                    }
                }
                Destroy(gameObject);
            }
        }
        else
        {
            promptText.SetActive(false);
            baloncukUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = false;
    }
}
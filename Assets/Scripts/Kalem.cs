using UnityEngine;
using UnityEngine.UI;

public class Kalem : MonoBehaviour
{// Baloncuk objesi
    public GameObject promptText; // "E'ye bas" gibi uyarı texti
    public Sprite kalemKagitSprite; // Inspector'dan ata
    private bool isPlayerNear = false;
    private PlayerMovement2 playerMovement2;

    public GameObject player2; // Kalem kağıt prefabı

    void Update()
    {
        Inventory inv = FindObjectOfType<Inventory>();
        bool hasKalem = inv != null && inv.items.Contains("Kalem");
        if (isPlayerNear)
        {
            promptText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                var skeleton = player2 != null ? player2.GetComponent<Spine.Unity.SkeletonAnimation>() : null;

                if (!hasKalem)
                {
                    // Kalem yoksa NoCrayon animasyonu oynat, sonra Idle2'ye dön
                    if (skeleton != null)
                    {
                        skeleton.AnimationState.SetAnimation(0, "NoCrayon", false);
                        skeleton.AnimationState.AddAnimation(0, "Idle2", true, 0f);
                    }
                    return;
                }
                promptText.SetActive(false);

                var playerMovement = player2.GetComponent<PlayerMovement2>();
                if (playerMovement != null)
                {
                    var skeleton2 = playerMovement.GetComponent<Spine.Unity.SkeletonAnimation>();
                    if (skeleton2 != null)
                        skeleton2.AnimationState.SetAnimation(0, "Idle1", false); // false = loop olmasın, bir kez oynasın
                }

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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            playerMovement2 = other.GetComponent<PlayerMovement2>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            playerMovement2 = null;
        }
    }
}
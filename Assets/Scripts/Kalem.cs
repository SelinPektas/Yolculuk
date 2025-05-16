using UnityEngine;

public class Kalem : MonoBehaviour
{
    public GameObject baloncukUI; // Baloncuk objesi
    private bool isPlayerNear = false;

    void Update()
    {
        Inventory inv = FindObjectOfType<Inventory>();
        bool hasKalem = inv != null && inv.items.Contains("Kalem");

        if (isPlayerNear)
        {
            baloncukUI.SetActive(true);

            if (hasKalem && Input.GetKeyDown(KeyCode.E))
            {
                baloncukUI.SetActive(false);

                // Kalemi envanterden çıkar
                inv.items.Remove("Kalem");

                // İstersen envanter UI'dan da kaldırabilirsin:
                foreach (Transform slot in inv.inventoryPanel)
                {
                    Image img = slot.GetComponentInChildren<Image>();
                    if (img != null && img.sprite.name == "kalemSpriteAdı") // Sprite adını doğru gir
                    {
                        Destroy(slot.gameObject);
                        break;
                    }
                }
            }
        }
        else
        {
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
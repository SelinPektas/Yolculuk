using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject cane;
    public Transform caneSlot;
    public GameObject promptUI;
    public TrainController trainController;

    public SpriteRenderer slotSpriteRenderer; // Slotun SpriteRenderer'ı
    public Sprite insertedSprite; // Baston takılı sprite
    public Sprite pushedSprite;   // Baston ileri itilmiş sprite

    private bool isCaneInserted = false;
    private bool isLeverPushed = false;
    private bool isPlayerNear = false;

    void Update()
    {
        // Baston envanterde mi kontrol et
        bool hasCaneInInventory = false;
        Inventory inv = FindObjectOfType<Inventory>();
        if (inv != null)
            hasCaneInInventory = inv.items.Contains("Baston"); // Bastonun ismini doğru yaz

        if (isPlayerNear && !isCaneInserted && hasCaneInInventory && Input.GetKeyDown(KeyCode.E))
        {
            isCaneInserted = true;
            promptUI.SetActive(true);

            if (slotSpriteRenderer != null && insertedSprite != null)
                slotSpriteRenderer.sprite = insertedSprite;
        }
        else if (isPlayerNear && isCaneInserted && !isLeverPushed && Input.GetKeyDown(KeyCode.E))
        {
            isLeverPushed = true;
            promptUI.SetActive(false);

            if (slotSpriteRenderer != null && pushedSprite != null)
                slotSpriteRenderer.sprite = pushedSprite;

            if (trainController != null)
                trainController.StartTrain();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && cane.activeInHierarchy && !isLeverPushed)
        {
            isPlayerNear = true;
            promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            promptUI.SetActive(false);
        }
    }
}
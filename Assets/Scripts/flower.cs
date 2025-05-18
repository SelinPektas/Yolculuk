using UnityEngine;

public class flower : MonoBehaviour
{
    public GameObject promptText;      // "E'ye basabilirsin" yazısı
    public Sprite flowerSprite;        // Inspector'dan ata
    private bool isPlayer2Near = false;

    void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);
    }

    void Update()
    {
        if (isPlayer2Near && Input.GetKeyDown(KeyCode.E))
        {
            // ENVANTERE EKLE
            Inventory inv = FindObjectOfType<Inventory>();
            if (inv != null && flowerSprite != null)
                inv.AddItem("Çiçek", flowerSprite);

            if (promptText != null)
                promptText.SetActive(false);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            if (promptText != null)
                promptText.SetActive(true);
            isPlayer2Near = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            if (promptText != null)
                promptText.SetActive(false);
            isPlayer2Near = false;
        }
    }
}
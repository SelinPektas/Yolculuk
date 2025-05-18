using UnityEngine;

public class flower : MonoBehaviour
{
    public GameObject promptText;      // "E'ye basabilirsin" yazısı
    public Sprite flowerSprite;        // Inspector'dan ata
    private bool isPlayer2Near = false;
    private Animator player2Animator;

    void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);

        // Player2'nin Animator'ını bul
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        if (player2 != null)
            player2Animator = player2.GetComponent<Animator>();
    }

    void Update()
    {
        if (isPlayer2Near && Input.GetKeyDown(KeyCode.E))
        {
            // Animasyonu çalıştır
            if (player2Animator != null)
                player2Animator.Play("pickflowers");

            // Envantere ekleme işlemi
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
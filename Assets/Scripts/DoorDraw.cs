using UnityEngine;

public class DoorDraw : MonoBehaviour
{
    public GameObject promptText;      // Yaklaşınca çıkacak text
    public GameObject doorPrefab;      // Eklenecek kapı sprite prefabı
    public GameObject aktifOlacakObje; // Aktif edilecek obje

    private bool isPlayer2Near = false;

    void Start()
    {
        promptText.SetActive(false);
        if (aktifOlacakObje != null)
            aktifOlacakObje.SetActive(false); // Başta kapalı olsun
    }

    void Update()
    {
        if (isPlayer2Near && Input.GetKeyDown(KeyCode.E))
        {
            // Kapıyı aynı pozisyona ekle
            Instantiate(doorPrefab, doorPrefab.transform.position, doorPrefab.transform.rotation);

            // İstediğin objeyi aktif et
            if (aktifOlacakObje != null)
                aktifOlacakObje.SetActive(true);

            promptText.SetActive(false);
            isPlayer2Near = false;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            promptText.SetActive(true);
            isPlayer2Near = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            promptText.SetActive(false);
            isPlayer2Near = false;
        }
    }
}
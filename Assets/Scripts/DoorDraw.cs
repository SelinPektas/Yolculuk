using UnityEngine;

public class DoorDraw : MonoBehaviour
{
    public GameObject promptText;      // Yaklaşınca çıkacak text
    public GameObject doorPrefab;      // Eklenecek kapı sprite prefabı
    public GameObject aktifOlacakObje; // Aktif edilecek obje
    public GameObject gosterilecekObje; // Collider'a girince görünecek obje

    private bool isPlayer2Near = false;

    void Start()
    {
        promptText.SetActive(false);
        if (aktifOlacakObje != null)
            aktifOlacakObje.SetActive(false); // Başta kapalı olsun
        if (gosterilecekObje != null)
            gosterilecekObje.SetActive(false); // Başta kapalı olsun
    }

    void Update()
    {
        if (isPlayer2Near && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(doorPrefab, doorPrefab.transform.position, doorPrefab.transform.rotation);

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
            isPlayer2Near = true; // Player2 girince göster
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (gosterilecekObje != null)
                gosterilecekObje.SetActive(true); // Player1 girince de göster
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            promptText.SetActive(false);
            isPlayer2Near = false; // Player2 çıkınca gizle
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (gosterilecekObje != null)
                gosterilecekObje.SetActive(false); // Player1 çıkınca da gizle
        }
    }
}
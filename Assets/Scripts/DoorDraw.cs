using UnityEngine;

public class DoorDraw : MonoBehaviour
{
    public GameObject player2;         // Player2 objesini Inspector'dan ata
    public GameObject promptText;      // Yaklaşınca çıkacak text
    public GameObject objectToDestroy; // Yok edilecek obje (ör: duvar)
    public GameObject doorPrefab;      // Eklenecek kapı sprite prefabı

    private bool isPlayer2Near = false;

    void Start()
    {
        promptText.SetActive(false);
    }

    void Update()
    {
        if (isPlayer2Near && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(objectToDestroy);

            // Kapıyı aynı pozisyona ekle
            Instantiate(doorPrefab, objectToDestroy.transform.position, Quaternion.identity);

            promptText.SetActive(false);
            isPlayer2Near = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player2)
        {
            promptText.SetActive(true);
            isPlayer2Near = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player2)
        {
            promptText.SetActive(false);
            isPlayer2Near = false;
        }
    }
}
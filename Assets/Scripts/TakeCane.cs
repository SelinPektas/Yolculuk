using UnityEngine;
using UnityEngine.UI;

public class TakeCane : MonoBehaviour
{
    public GameObject cane;
    public GameObject promptUI;

    public string caneName = "Baston";
    public Sprite caneIcon;

    private bool hasCane = false;
    private bool isPlayerNear = false;

    void Start()
    {
        cane.SetActive(false);
        promptUI.SetActive(false); // Başlangıçta yazı gizli
    }

    void Update()
    {
        if (isPlayerNear && !hasCane && Input.GetKeyDown(KeyCode.E))
        {
            cane.SetActive(true);
            hasCane = true;
            promptUI.SetActive(false);

            // Bastonu envantere ekle
            Inventory inv = FindObjectOfType<Inventory>();
            if (inv != null)
            {
                inv.AddItem(caneName, caneIcon);
            }

            Destroy(gameObject); // Baston objesini yok et
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasCane)
        {
            promptUI.SetActive(true);
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            promptUI.SetActive(false);
            isPlayerNear = false;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class TakeCane : MonoBehaviour
{
    public GameObject promptUI;
    public string caneName = "Baston";
    public Sprite caneIcon;
    public GameObject kalemObjesi; // Inspector'dan ata

    private bool hasCane = false;
    private bool isPlayerNear = false;
    public GameObject aktiflesecekObje; // Inspector'dan ata


    void Start()
    {
        promptUI.SetActive(false);
        if (kalemObjesi != null)
            kalemObjesi.SetActive(false); // Başta kalem gizli olsun
    }

    void Update()
    {
        if (isPlayerNear && !hasCane && Input.GetKeyDown(KeyCode.E))
        {
            if (aktiflesecekObje != null)
                aktiflesecekObje.SetActive(true);

            hasCane = true;
            promptUI.SetActive(false);

            // Bastonu envantere ekle
            Inventory inv = FindObjectOfType<Inventory>();
            if (inv != null)
                inv.AddItem(caneName, caneIcon);

            // PlayerMovement'a haber ver
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                var movement = player.GetComponent<PlayerMovement>();
                if (movement != null)
                    movement.SetHasCane(true);
            }

            // Kalem objesini aktif et
            if (kalemObjesi != null)
                kalemObjesi.SetActive(true);



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
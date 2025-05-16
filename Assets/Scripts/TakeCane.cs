using UnityEngine;
using UnityEngine.UI;

public class TakeCane : MonoBehaviour
{
    public GameObject cane;
    public GameObject promptUI;

    public string caneName = "Baston";
    public Sprite caneIcon;

    private bool hasCane = false;

    void Start()
    {
        cane.SetActive(false);
        promptUI.SetActive(true);
    }

    void Update()
    {
        if (!hasCane && Input.GetKeyDown(KeyCode.E))
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
        }
    }
}
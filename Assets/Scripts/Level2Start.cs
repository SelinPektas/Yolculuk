using UnityEngine;

public class Level2Start : MonoBehaviour
{
    public Sprite bastonSprite;      // Inspector'dan ata
    public Sprite kalemKagitSprite;  // Inspector'dan ata

    void Start()
    {
        Inventory inv = FindObjectOfType<Inventory>();
        if (inv != null)
        {
            inv.AddItem("Baston", bastonSprite);
            inv.AddItem("Kalem Kağıt", kalemKagitSprite);
        }
    }
}
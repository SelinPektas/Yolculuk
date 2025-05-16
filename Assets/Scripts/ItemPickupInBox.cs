using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickupInBox : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public Sprite itemIcon;

    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory inv = FindObjectOfType<Inventory>();
        if (inv != null)
        {
            inv.AddItem(itemName, itemIcon);
            gameObject.SetActive(false); // Eşya kutudan kaybolur
        }
    }
}
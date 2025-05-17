using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPickupInBox : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public Sprite itemIcon;
    public Sprite openedBoxSprite;

    public Image boxImage;

    private bool isOpened = false; // Sadece bir kez tıklansın

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOpened) return; // Daha önce açıldıysa tekrar çalışmasın
        isOpened = true;

        Inventory inv = FindObjectOfType<Inventory>();
        if (inv != null)
        {
            inv.AddItem(itemName, itemIcon);
            if (boxImage != null && openedBoxSprite != null)
                boxImage.sprite = openedBoxSprite;
        }
    }
}
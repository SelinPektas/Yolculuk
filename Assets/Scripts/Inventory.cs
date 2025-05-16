using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    public Transform inventoryPanel; // UI paneli
    public GameObject itemSlotPrefab; // Sadece Image içeren prefab

    public void AddItem(string itemName, Sprite itemIcon)
    {
        items.Add(itemName);

        // UI'da sadece resmi göster
        GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel);
        slot.GetComponentInChildren<Image>().sprite = itemIcon;
        slot.name = "Slot_" + itemName; // Slotun ismini item ismine göre ayarla
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
            items.Remove(itemName);

        // Slotu bul ve sil
        Transform slotToRemove = inventoryPanel.Find("Slot_" + itemName);
        if (slotToRemove != null)
            Destroy(slotToRemove.gameObject);
    }
}
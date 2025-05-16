using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    public Transform inventoryPanel;
    public GameObject itemSlotPrefab;

    public void AddItem(string itemName, Sprite itemIcon)
    {
        items.Add(itemName);
        GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel);
        slot.GetComponentInChildren<Image>().sprite = itemIcon;
        slot.name = "Slot_" + itemName;
    }

    public void RemoveItem(string itemName)
    {
        if (items.Contains(itemName))
            items.Remove(itemName);

        Transform slotToRemove = inventoryPanel.Find("Slot_" + itemName);
        if (slotToRemove != null)
            Destroy(slotToRemove.gameObject);
    }
}
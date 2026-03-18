using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public Image[] slots; // UI slot
    private Item[] items;

    void Start()
    {
        items = new Item[slots.Length];
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                slots[i].sprite = item.icon;
                slots[i].enabled = true;
                return;
            }
        }

        Debug.Log("Hotbar penuh!");
    }
}
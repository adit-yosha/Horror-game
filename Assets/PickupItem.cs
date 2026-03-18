using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Hotbar hotbar;

    private bool isNear = false;
    private static PickupItem currentItem; // item yg lagi bisa diambil

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            currentItem = this;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            if (currentItem == this)
                currentItem = null;
        }
    }

    // 🔥 DIPANGGIL DARI BUTTON
    public static void Pickup()
    {
        if (currentItem != null)
        {
            Item item = currentItem.GetComponent<Item>();

            if (item != null)
            {
                currentItem.hotbar.AddItem(item);
                Destroy(currentItem.gameObject);
                currentItem = null;
            }
        }
    }
}
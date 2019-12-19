using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool craftable;
    public bool unique;
    public UnityEvent thisEvent;
    public List<InventoryItem> components = new List<InventoryItem>();

    public void Craft()
    {
        if (VerifyComponents())
        {
            thisEvent.Invoke();
            numberHeld++;
        }
    }

    private bool VerifyComponents()
    {
        if (components.Count > 0)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].numberHeld <= 0)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public void DecreaseAmount(int amountToDecrease)
    {
        numberHeld-= amountToDecrease;
        if (numberHeld < 0)
        {
            numberHeld = 0;
        }
    }
}

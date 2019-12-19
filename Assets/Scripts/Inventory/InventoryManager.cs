using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject createButton;
    public InventoryItem currentItem;

    public void SetButton(bool buttonActive)
    {
        if (buttonActive)
        {
            createButton.SetActive(true);
        }
        else
        {
            createButton.SetActive(false);
        }
    }

    void MakeInventorySlot()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if (playerInventory.myInventory[i].numberHeld > 0 || playerInventory.myInventory[i].craftable)
                {
                    GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                    temp.transform.SetParent(inventoryPanel.transform);
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.myInventory[i], this);
                    }
                }
            }
        }
    }
    
    // Start is called before the first frame update
    void OnEnable()
    {
        ClearInventorySlot();
        MakeInventorySlot();
        SetButton(false);
    }

    public void SetupButton(bool isButtonCraftable, InventoryItem newItem)
    {
        currentItem = newItem;
        createButton.SetActive(isButtonCraftable);
    }

    void ClearInventorySlot()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }
    public void CraftbuttonPressed()
    {
        currentItem.Craft();
        ClearInventorySlot();
        MakeInventorySlot();
        if (currentItem.numberHeld == 0 && !currentItem.craftable)
        {
            SetButton(false);
        }
    }
}

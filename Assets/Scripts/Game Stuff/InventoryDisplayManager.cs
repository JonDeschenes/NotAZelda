using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayManager : MonoBehaviour
{
    public GameObject inventoryPanel;

    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                inventoryPanel.SetActive(true);
                isOpen = true;
            }
            else
            {
                inventoryPanel.SetActive(false);
                isOpen = false;
            }
        }
    }
}

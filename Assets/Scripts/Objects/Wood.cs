using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : PowerUp
{
    public Inventory playerInventory;
    
    void Start()
    {
        powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{

    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;
    public float maxPlayerHealth;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && playerHealth.RunTimeValue < maxPlayerHealth)
        {
            SoundManager.PlaySound("Health");
            playerHealth.RunTimeValue = (playerHealth.RunTimeValue + amountToIncrease > maxPlayerHealth) 
                ? maxPlayerHealth 
                : playerHealth.RunTimeValue += amountToIncrease;
            if (playerHealth.initialValue > heartContainers.RunTimeValue * 2f)
            {
                playerHealth.initialValue = heartContainers.RunTimeValue * 2f;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;
    public Vector3 playerChange;
    private CameraMovement camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            camera.minPosition += cameraChangeMin;
            camera.maxPosition += cameraChangeMax;
            other.transform.position += playerChange;
        }
    }
}

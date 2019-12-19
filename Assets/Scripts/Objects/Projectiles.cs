using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 directionToMove;
    public float lifeTime;
    private float lifeTimeSeconds;
    public Rigidbody2D myRigidBody;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        lifeTimeSeconds = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeSeconds -= Time.deltaTime;
        if (lifeTimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialVel)
    {
        myRigidBody.velocity = initialVel * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}

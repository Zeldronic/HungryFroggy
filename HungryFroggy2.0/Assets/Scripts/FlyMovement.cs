using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float maxSpeed = 3f;
    Vector2 movement;
    int frame=0;

    // Update is called once per frame
    void Update()
    {
        frame++;
        if (frame == 10)
        {
            movement = new Vector2(Random.Range(-1, 2), Random.Range(-2, 3));
            Debug.Log("movement= (" + movement.x + ", " + movement.y + ")");
            frame = 0;
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(movement * maxSpeed);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BounceOnCollision : MonoBehaviour
{
    public float bounceForce = 10f;
    private Rigidbody2D rb;
    Vector3 lastVelocity;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if colliding with player or not:
        if (collision.gameObject.CompareTag("Player")) {
            var speed = lastVelocity.magnitude + collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
            //Colliding Object
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * speed * (collision.gameObject.GetComponent<Rigidbody2D>().mass / (collision.gameObject.GetComponent<Rigidbody2D>().mass+rb.mass));
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = -direction * speed * (rb.mass / (collision.gameObject.GetComponent<Rigidbody2D>().mass + rb.mass));
        }
        else
        {
            Debug.Log(collision.gameObject.tag);
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * speed;
        }
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }
}

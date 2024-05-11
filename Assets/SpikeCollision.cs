using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check if colliding with player or not:
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().health -= 20;
            FindObjectOfType<AudioManager>().Play("Trap");
            DamagePopup.Create(collision.gameObject.transform.position, 20, false);
        }
        else
        {
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}

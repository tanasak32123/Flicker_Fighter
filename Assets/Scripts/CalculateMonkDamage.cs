using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateMonkDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    public float dodgeRate = 0.1f;
private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!player.isTurn)
            {
                if (collision.gameObject.GetComponent<Player>().team != player.team)
                {
                    if (Random.value < dodgeRate)
                    {
                        FindObjectOfType<AudioManager>().Play("Dodge");
                        TextPopup.Create(collision.gameObject.transform.position, "DODGED!",Color.blue);
                        return; 
                    }
                    else
                    {
                        if (Random.value < collision.gameObject.GetComponent<Player>().critChance){
                        Debug.Log("Crit");
                        FindObjectOfType<AudioManager>().Play("Crit");
                        player.health = player.health - collision.gameObject.GetComponent<Player>().attack * 2;
                    }
                        else{
                        FindObjectOfType<AudioManager>().Play("Hit");
                        DamagePopup.Create(collision.gameObject.transform.position,((int)collision.gameObject.GetComponent<Player>().attack),false);
                        player.health = player.health - collision.gameObject.GetComponent<Player>().attack;
                    }
                    }
                }
            }
        };
    }

    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

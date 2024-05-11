
using UnityEngine;

public class CalculateDamage : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!player.isTurn)
            {
                if (collision.gameObject.GetComponent<Player>().team != player.team)
                {
                    if (Random.value < collision.gameObject.GetComponent<Player>().critChance){
                        Debug.Log("Crit");
                        FindObjectOfType<AudioManager>().Play("Crit");
                        player.health = player.health - collision.gameObject.GetComponent<Player>().attack * 2;
                        DamagePopup.Create(collision.gameObject.transform.position, ((int)collision.gameObject.GetComponent<Player>().attack*2), true);
                    }
                    else{
                        FindObjectOfType<AudioManager>().Play("Hit");
                        player.health = player.health - collision.gameObject.GetComponent<Player>().attack;
                        DamagePopup.Create(collision.gameObject.transform.position, ((int)collision.gameObject.GetComponent<Player>().attack), false);
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

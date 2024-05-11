using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollision : MonoBehaviour
{
    public GameObject ChainPrefab;
    [SerializeField] private float TrapDamage = 40;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if colliding with player or not:
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            HandlePlayerHitTrap(player);

            FindObjectOfType<AudioManager>().Play("Trap");
            Destroy(gameObject);
            DamagePopup.Create(collision.gameObject.transform.position, 50, false);
            TextPopup.Create(collision.gameObject.transform.position, "ROOTED!", Color.white);
        }
    }

    private void HandlePlayerHitTrap(Player player) {
        player.transform.position = transform.position;
        player.RemainImmovabilityTurn = 3;
        player.SetHealth(player.health - TrapDamage);

        Instantiate(ChainPrefab, player.transform);

        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

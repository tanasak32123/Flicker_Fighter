using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningScript : MonoBehaviour
{
    private List<Player> playersInArea;
    private GlobalManagerSc _globalManagerSC;
    private int remainingTurn;
    private int currentTurn;
    private bool doAOEDamage;
    private float skillRadius;
    private float hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        remainingTurn = 2;
        skillRadius = 4f;
        hitPoint = 20f;
        _globalManagerSC = FindAnyObjectByType<GlobalManagerSc>();
        currentTurn = _globalManagerSC.turnNumber;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTurn < _globalManagerSC.turnNumber && !doAOEDamage)
        {
            remainingTurn -= _globalManagerSC.turnNumber - currentTurn;

            BurningPlayers();

            if (remainingTurn <= 0)
            {
                Destroy(gameObject);
            }

            currentTurn = _globalManagerSC.turnNumber;
            doAOEDamage = false;
        }
    }

    private void BurningPlayers()
    {
        doAOEDamage = true;
        playersInArea = DetectPlayerInArea();
        foreach (Player player in playersInArea)
        {
            player.SetHealth(player.health - hitPoint);
            DamagePopup.Create(player.gameObject.transform.position, 20, false);
        }
    }

    private List<Player> DetectPlayerInArea()
    {
        List<Player> players = new List<Player>();
        LayerMask mask = LayerMask.GetMask("Player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skillRadius, mask);
        foreach (Collider2D collider in colliders)
        {
            Player p = collider.gameObject.GetComponent<Player>();
            if (!players.Contains(p)) players.Add(p);
        }
        return players;
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, 4f);
    //}
}

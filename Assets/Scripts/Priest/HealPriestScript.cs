using System.Collections.Generic;
using UnityEngine;

public class HealPriestScript : MonoBehaviour
{
    // private GlobalManagerSc manager;

    private GameObject HealIndicator;
    public GameObject HealButton;

    [SerializeField] private int healPoint;
    [SerializeField] private Player player;
    [SerializeField] private float skillRadius;

    List<Player> playersInRadius;
    private bool isAlreadyDetected;
    private bool isActivateSkill;

    [SerializeField] private float sacrificeHealth = 60;

    // Start is called before the first frame update
    void Start()
    {
        // manager = GameObject.Find("GameManager").GetComponent<GlobalManagerSc>();

        HealIndicator = gameObject.transform.GetChild(0).gameObject;
        skillRadius = 10f;
        healPoint = 50;
        player = gameObject.transform.parent.parent.gameObject.GetComponent<Player>();

        HealButton.SetActive(false);
        HealIndicator.SetActive(false);
    }

    void Update()
    {
        if (isActivateSkill)
        {
            if (!player.isTurn) {
                DeactivateHealAbility();
            }

            if (!isAlreadyDetected)
            {
                playersInRadius = DetectPlayerInArea();
                HighlightPlayers(playersInRadius, Color.yellow);
                isAlreadyDetected = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                player.IsUseAbilityOnTurn = true;
                player.SetHealth(player.health - sacrificeHealth);
                HealAllies();
                // manager.switchTurn();
                DeactivateHealAbility();
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                DeactivateHealAbility();
            }
        }
    }

    public void HealAllies()
    {
        foreach (Player player in playersInRadius)
        {
            player.SetHealth(player.health + healPoint);
            player.SetColorPlayerToDefault();
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
            if (
                p.GetInstanceID() != player.GetInstanceID() &&
                p.team == player.team
            ) {
                if (!players.Contains(p)) players.Add(p);
            }
        }
        return players;
    }

    private void HighlightPlayers(List<Player> players, Color color)
    {
        foreach (Player p in players)
        {
            p.SetColorPlayer(color);
        }
    }

    private void ActivateHealAbility()
    {
        isActivateSkill = true;
        player.IsActivateAbility = true;
        HealIndicator.SetActive(true);
    }

    private void DeactivateHealAbility()
    {
        isActivateSkill = false;
        player.IsActivateAbility = false;
        isAlreadyDetected = false;
        HealIndicator.SetActive(false);
    }

    public void OnClickHealSkill()
    {
        if (!player.IsUseAbilityOnTurn && !isActivateSkill && !player.IsMoving)
        {
            ActivateHealAbility();
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, 10f);
    //}
}

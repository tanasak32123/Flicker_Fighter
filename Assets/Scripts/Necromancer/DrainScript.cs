using UnityEngine;
using UnityEngine.UI;

public class DrainScript : MonoBehaviour
{
    // private GlobalManagerSc manager;
    private LayerMask mask;
    private Player player;
    private GameObject DrainIndicator;
    private GameObject DrainButton;
    private bool isActivated;
    private Player selectedPlayer;
    [SerializeField] private readonly float drainPoint = 50;

    private int numberOfCast = 2;

    // Start is called before the first frame update
    void Start()
    {
        // manager = GameObject.Find("GameManager").GetComponent<GlobalManagerSc>();
        mask = LayerMask.GetMask("Player");
        player = gameObject.transform.parent.parent.gameObject.GetComponent<Player>();
        DrainIndicator = gameObject.transform.GetChild(0).gameObject;
        DrainButton = player.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        
        DeActivateDrainAbility();
        DrainButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated) {
            HandleClickOnPlayer();

            if (!player.isTurn) {
                DeActivateDrainAbility();
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) {
                DeActivateDrainAbility();
            }
        }
    }

    void HandleClickOnPlayer() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 1, mask);
        if (hit && Vector2.Distance(transform.position, hit.collider.gameObject.transform.position) <= 10f) {
            Player player = hit.collider.gameObject.GetComponent<Player>();
            if (selectedPlayer != player) {
                if (selectedPlayer) {
                    selectedPlayer.SetColorPlayerToDefault();
                }
                selectedPlayer = player;
                selectedPlayer.SetColorPlayer(Color.yellow);
            }
        } else if (!hit && selectedPlayer) {
            selectedPlayer.SetColorPlayerToDefault();
            selectedPlayer = null;
        }
        if (Input.GetMouseButtonDown(0) && selectedPlayer) {
            selectedPlayer.SetHealth(selectedPlayer.health - drainPoint);
            player.SetHealth(player.health + drainPoint);
            // manager.switchTurn();
            numberOfCast -= 1;
            FindObjectOfType<AudioManager>().Play("Necromancer");
            DeActivateDrainAbility();
            if (numberOfCast == 0) {
                Button btn = DrainButton.GetComponent<Button>();
                btn.interactable = false;
            }
            player.IsUseAbilityOnTurn = true;
        }
    }

    void ActivateDrainAbility() {
        isActivated = true;
        player.IsActivateAbility = true;
        DrainIndicator.SetActive(true);
    }

    void DeActivateDrainAbility() {
        if (selectedPlayer) {
            selectedPlayer.SetColorPlayerToDefault();
            selectedPlayer = null;
        }
        isActivated = false;
        player.IsActivateAbility = false;
        DrainIndicator.SetActive(false);
    }

    public void onClickDrainButton()
    {
        if (!isActivated && 
        !player.IsMoving && 
        !player.IsUseAbilityOnTurn &&
        numberOfCast > 0
        ) {
            ActivateDrainAbility();
        }
    } 

    //  void OnDrawGizmos()
    // {
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, 10f);
    // }
}

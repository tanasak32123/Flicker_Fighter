
using UnityEngine;

public class HunterScript : MonoBehaviour
{
    // private GlobalManagerSc manager;
    private LayerMask foregroundMask;

    private Player player;

    private GameObject TrapButton;
    private GameObject TrapIndicator;
    private GameObject TrapSizeIndicator;
    public GameObject TrapPrefab;

    private SpriteRenderer playerSprite;
    private SpriteRenderer TrapSizeIndicatorSprite;
    private SpriteRenderer TrapAvailableAreaIndicatorSprite;

    private bool isActivated;

    [SerializeField] private float sacrificeHealth = 30;

    // Start is called before the first frame update
    void Start()
    {
        // manager = GameObject.Find("GameManager").GetComponent<GlobalManagerSc>();
        foregroundMask = LayerMask.GetMask("Foreground");
        player = gameObject.transform.parent.parent.gameObject.GetComponent<Player>();
        playerSprite = gameObject.transform.parent.parent.gameObject.GetComponent<SpriteRenderer>();

        TrapIndicator = gameObject.transform.GetChild(0).gameObject;
        TrapSizeIndicator = TrapIndicator.transform.GetChild(0).gameObject;
        TrapSizeIndicatorSprite = TrapSizeIndicator.GetComponent<SpriteRenderer>();
        TrapAvailableAreaIndicatorSprite = TrapIndicator.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        TrapButton = gameObject.transform.parent.GetChild(0).GetChild(0).gameObject;
        
        TrapButton.SetActive(false);
        DeActivateTrapAbility();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated) {
            HandleShowIndicator();
            HandlePlacingTrap();

            if (!player.isTurn) {
                DeActivateTrapAbility();
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) {
                DeActivateTrapAbility();
            }
        }
    }
    
    void HandleShowIndicator() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float radiusOfAvailableArea = TrapAvailableAreaIndicatorSprite.bounds.size.x / 2;
        float radiusOfTrapSize = TrapSizeIndicatorSprite.bounds.size.x / 2;
        float radiusOfPlayer = playerSprite.bounds.size.x / 2;

        float playerToMouseDistance = Vector2.Distance(mousePos, transform.position);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.up, 0, foregroundMask);
        if (
            radiusOfPlayer + radiusOfTrapSize <= playerToMouseDistance &&
            playerToMouseDistance <= radiusOfAvailableArea - radiusOfTrapSize &&
            hit.collider == null
        ) {
            if (!TrapSizeIndicator.activeInHierarchy) TrapSizeIndicator.SetActive(true);

            TrapSizeIndicator.transform.position = mousePos;
        } 
        
        if (
            TrapSizeIndicator.activeInHierarchy &&
            radiusOfPlayer + radiusOfTrapSize > playerToMouseDistance && 
            hit.collider != null
        ) {
            TrapSizeIndicator.SetActive(false);
        }
    }

    void HandlePlacingTrap() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float playerToMouseDistance = Vector2.Distance(mousePos, transform.position);
        float radiusOfAvailableArea = TrapAvailableAreaIndicatorSprite.bounds.size.x / 2;
        float radiusOfTrapSize = TrapSizeIndicatorSprite.bounds.size.x / 2;
        float radiusOfPlayer = playerSprite.bounds.size.x / 2;
        if (
            TrapSizeIndicator.activeInHierarchy && Input.GetMouseButtonDown(0) &&
            radiusOfPlayer + radiusOfTrapSize <= playerToMouseDistance &&
            playerToMouseDistance <= radiusOfAvailableArea - radiusOfTrapSize
        ) {
            Instantiate(TrapPrefab, TrapSizeIndicator.transform.position, TrapSizeIndicator.transform.rotation);
            FindObjectOfType<AudioManager>().Play("Hunter");
            player.SetHealth(player.health - sacrificeHealth);
            player.IsUseAbilityOnTurn = true;
            // manager.switchTurn();
            DeActivateTrapAbility();
        }
    }

    void ActivateTrapAbility() {
        isActivated = true;
        player.IsActivateAbility = true;
        TrapIndicator.SetActive(true);
    }

    void DeActivateTrapAbility() {
        isActivated = false;
        player.IsActivateAbility = false;
        TrapIndicator.SetActive(false);
    }

    public void OnClickTrapButton()
    {
        if (!player.IsUseAbilityOnTurn && !isActivated && !player.IsMoving) {
            ActivateTrapAbility();
        }
    } 

    //  void OnDrawGizmos()
    // {
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, TrapAvailableAreaIndicatorSprite.bounds.size.x / 2);
    // }
}

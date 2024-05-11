using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private GlobalManagerSc manager;
    public Rigidbody2D playerRigidbody2D;
    private TrajectoryLine trajectoryLine;
    private LookAtMouse lookAtMouse;

    private GameObject currentFireballObject;
    private Player player;
    private GameObject FireballSprite;
    private GameObject FireballButton;

    public GameObject FireballPrefab;
    public GameObject BurningPrefab;

    public bool IsActivated { get; set; }
    public bool IsToggle { get; set; }

    public float power = 3f;
    private Vector2 force;
    private Vector3 startPoint;
    private Vector3 currentPoint;
    private Vector3 endPoint;
 
    [SerializeField] private float speed = 5;
    [SerializeField] private Vector2 minPower;
    [SerializeField] private Vector2 maxPower;
    [SerializeField] private float sacrificeHealth = 40;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GlobalManagerSc>();

        player = gameObject.transform.parent.parent.gameObject.GetComponent<Player>();
        FireballSprite = gameObject.transform.GetChild(0).gameObject;
        FireballButton = player.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        
        trajectoryLine = player.GetComponent<TrajectoryLine>();

        lookAtMouse = FireballSprite.GetComponent<LookAtMouse>();
        
        maxPower = new Vector2(10f, 10f);
        minPower = new Vector2(-10f, -10f);

        IsActivated = false;
        IsToggle = false;

        lookAtMouse.enabled = false;
        FireballSprite.SetActive(false);
        FireballButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated && !IsToggle)
        {
            if (!player.isTurn) {
                DeactivateFireballAbility();
            }

            if (currentFireballObject == null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startPoint = player.transform.position;
                    startPoint.z = -3;
                    lookAtMouse.enabled = false;
                }

                if (Input.GetMouseButton(0))
                {
                    currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    Vector3 dir = startPoint - currentPoint;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    FireballSprite.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    currentPoint.z = -3;
                    trajectoryLine.RenderLine(startPoint, currentPoint);
                }

                if (
                    Input.GetMouseButtonUp(0)
                )
                {
                    player.IsUseAbilityOnTurn = true;
                    
                    FireballSprite.SetActive(false);

                    player.SetHealth(player.health - sacrificeHealth);

                    DeactivateFireballAbility();

                    var dir = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    FindObjectOfType<AudioManager>().Play("Fireball");
                    currentFireballObject = Instantiate(
                        FireballPrefab,
                        new Vector3(transform.position.x, transform.position.y, 0),
                        Quaternion.Euler(dir.x, dir.y, dir.z),
                        gameObject.transform
                    );

                    currentFireballObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    endPoint.z = -3;

                    force = new Vector2(
                        Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                        Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y)
                    );
                    currentFireballObject.GetComponent<Rigidbody2D>().AddForce(force * power, ForceMode2D.Impulse);

                    manager.cam.setTarget(currentFireballObject.transform);

                    trajectoryLine.EndLine();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                DeactivateFireballAbility();
            }
        }

        if (IsToggle) IsToggle = false;
    }

    private void ActivateFireballAbility()
    {
        playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        trajectoryLine.RemoveLine();
        FireballSprite.SetActive(true);
        IsToggle = true;
        IsActivated = true;
        lookAtMouse.enabled = true;
        player.IsActivateAbility = true;
    }

    private void DeactivateFireballAbility()
    {
        playerRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        FireballSprite.SetActive(false);
        IsActivated = false;
        player.IsActivateAbility = false;
        lookAtMouse.enabled = false;
        trajectoryLine.RemoveLine();

        if (currentFireballObject != null) Destroy(currentFireballObject);
    }

    public void OnPressedFireBall()
    {
        if (!player.IsUseAbilityOnTurn && !IsActivated && !player.IsMoving)
        {
            ActivateFireballAbility();
        }
    }
}

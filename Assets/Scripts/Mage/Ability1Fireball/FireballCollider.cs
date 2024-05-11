using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCollider : MonoBehaviour
{
    private GlobalManagerSc manager;
    public GameObject BurnPrefab;

    private Rigidbody2D _rigidBody2D;

    [SerializeField] private int hitDamage;

    public bool IsMoving { get; set; }
    private bool IsThrow;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GlobalManagerSc>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        hitDamage = 60;
        IsMoving = true;
        IsThrow = false;
    }

    void Update()
    {
        if (IsMoving)
        {
            var magnitude = _rigidBody2D.velocity.magnitude;
            if (magnitude <= 2)
            {
                Explosion();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsThrow && IsMoving)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.health -= hitDamage;
                DamagePopup.Create(player.gameObject.transform.position, 60, false);
            }
            Explosion();
        }
        if (!IsThrow) IsThrow = true;
    }

    private void Explosion()
    {
        IsMoving = false;
        Vector3 destination = gameObject.transform.position;
        manager.cam.setTarget(gameObject.transform.parent.parent.parent.transform);
        // manager.switchTurn();
        Instantiate(
            BurnPrefab,
            destination,
            gameObject.transform.rotation
        );
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour

{
    private Vector3 localScale;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<Player>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.health / player.maxHealth);
        localScale.x = player.health/player.maxHealth;
        transform.localScale = localScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    public string pwName = "";
    public float point = 3;
    public float reducePerHit = 3;
    public TextMeshProUGUI pointLabel;

    protected void check()
    {
        point = point > 0 ? point : 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if colliding with player or not:
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        handleCollisPlayer(collision.gameObject.GetComponent<Player>());
        point = point - reducePerHit;
        updateLabel();
        handleAfterCollision();
    }
    protected abstract void handleCollisPlayer(Player player);

    protected void handleAfterCollision()
    {
        if (point <= 0)
        {
            die();
        }
    }
    
    public void die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void updateLabel()
    {
        if (pointLabel == null) { return; }
        pointLabel.text = point.ToString("F0");
    }

}

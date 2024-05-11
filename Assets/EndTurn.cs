using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EndTurn : MonoBehaviour
{
    public Player[] players;
    public Button button;
    [SerializeField] private SmoothCamera camScript;
    public void OnButtonClick()
    {
        if (button.interactable)
        {
            // Add your custom logic here
            foreach (Player p in players)
            {
                p.currentAttack = p.attack;
                p.gameObject.SetActive(false);
                p.gameObject.SetActive(true);
                p.gameObject.GetComponent<Player>().moved = false;
            }
            int pos = Array.FindIndex(players, p => p.isTurn == true);
            //Debug.Log(pos);
            players[pos].isTurn = false;
            players[(pos + 1) % players.Length].isTurn = true;
            players[(pos + 1) % players.Length].rb.velocity = Vector2.zero;
            players[(pos + 1) % players.Length].rb.angularVelocity = 0;
            camScript.setTarget(players[(pos + 1) % players.Length].transform);
        }
    }
    public void Start()
    {
        this.button = GetComponent<Button>();
        this.players = FindObjectsOfType<Player>();
        //UnityEngine.Debug.Log("get button", this.button);
        InvokeRepeating("CheckStop", 1f, 1f);  //1s delay, repeat every 1s

    }
    public void CheckStop()
    {
        bool done = true;
        Debug.Log(button);
        foreach (Player p in players) 
        {
            Debug.Log(p.name + " : "+ p.rb.totalForce);   
            if (p.rb.velocity.sqrMagnitude > 1 && p.rb.angularVelocity > 1) {
                done = false; break;
            }
        }
        if (done)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

    }
}

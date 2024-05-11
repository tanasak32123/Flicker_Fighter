using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerSc : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    public List<Sprite> playerSprites = new List<Sprite>();
    public List<Transform> pos = new List<Transform>();
    public GlobalManagerSc manager;
    public int team;
    private int index = 0;
    private bool inTurn = false;

    // Start is called before the first frame update
    public void setTeam(int t)
    {
        team = t;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].setTeam(team);
        }
    }

    private void updateCurrentPlayer()
    {
        clearFlagAll();
        players[index].isTurn = true;
        players[index].moved = false;
        players[index].DisplayAbilityButtons();
        manager.updateTurnUi(team, index + 1, players.Count, players[index].ballName);
        manager.cam.target = players[index].transform;
    }

    public void startTurn()
    {
        inTurn = true;
        Debug.Log("start turn team:"+ team.ToString());
        index = (index >= players.Count && players.Count != 0) ? index % players.Count : index;
        index = (players.Count == 0 || index < 0)? 0 : index;

        foreach (Player player in players) {
            player.CooldownAllPlayerBuff();
            player.IsUseAbilityOnTurn = false;
        }

        updateCurrentPlayer();
    }


    public void cycleChars(bool isInc = true)
    {
        if (!inTurn || players[index] == null || players[index].moved)
        {
            return;
        }

        if (isInc)
        {
            index++;
        }
        else
        {
            index--;
        }

        index = (index >= players.Count && players.Count != 0) ? index % players.Count : index;
        index = (index < 0 && players.Count != 0) ? players.Count + index : index;
        index = (players.Count == 0 || index < 0) ? 0 : index;

        updateCurrentPlayer();
    }


    public float getMovingVelocity()
    {
        return players[index].rb.velocity.magnitude;
    }

    public float getCurrentHp()
    {
        return players[index].health;
    }

    public float getCurrentAtk()
    {
        return players[index].attack;
    }

    public void endTurn()
    {
        inTurn = false;
        clearFlagAll();
    }

    public void addPlayerChar(Player player,Sprite image)
    {
        if (players.Count >= 5)
        {
            return;
        }

        player.transform.position = pos[players.Count % pos.Count].transform.position;
        players.Add(player);
        playerSprites.Add(image);
        Debug.Log(playerSprites.Count);
    }

    public void popPlayerChar() {
        if (players.Count < 1)
        {
            return;
        }
        var temp = players[players.Count - 1];
        players.RemoveAt(players.Count - 1);
        Destroy(temp.gameObject);
        var tempSprite = playerSprites[playerSprites.Count - 1];
        playerSprites.RemoveAt(playerSprites.Count - 1);
        Debug.Log(playerSprites.Count);
    }

    private void autoEndTurn()
    {
        if (!inTurn || players[index] == null || !players[index].moved)
        {
            return;
        }
        if (getMovingVelocity() < 0.1)
        {
            manager.switchTurn();
        }
    }

    private void clearFlagAll()
    {
        for (int i = 0; i < players.Count; i++) {
            players[i].isTurn = false;
            players[i].moved = false;
            players[i].HideAbilityButtons();
        }
    }

    private void clearDeadChar()
    {
        List<int> toRemove = new List<int>();
        for(int i = 0;i < players.Count ;i++)
        {
            if (players[i] == null)
            {
                toRemove.Add(i);
            }

            if (players[i].health <= 0) {
                if (players[i].isTurn)
                {
                    Debug.Log("die");
                    manager.switchTurn(true);
                }
                players[i].die();
                toRemove.Add(i);
            }
        }
        toRemove.ForEach
            (i => { 
                players.RemoveAt(i);
            });
    }

    void Update()
    {
        if (!manager.isStart)
        {
            return;
        }

        clearDeadChar();

        if (!inTurn)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            cycleChars(false);
        }
        else if (Input.GetKeyUp(KeyCode.D)) 
        {
            cycleChars(true); 
        }

        //autoEndTurn();
    }
}

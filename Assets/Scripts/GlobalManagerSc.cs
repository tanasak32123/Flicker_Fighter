using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalManagerSc : MonoBehaviour
{
    public PlayerManagerSc p1;
    public PlayerManagerSc p2;
    public SmoothCameraSc cam;
    public UiManagerSc uiMgr;
    public EndGameUiSc endGameUi;
    public CharSectorUISc charSelectUi;
    public int turnNumber = 0;
    public bool isStart = false;
    private int turnIndex = 0;
    private PlayerManagerSc current;

    void Start()
    {
        setInGameMenuVisible(false);
        setEndGameMenuVisible(false);
    }

    private void FixedUpdate()
    {
        if (!isStart)
        {
            return;
        }

        if (current == null)
        {
            return;
        }

        float hp = current.getCurrentHp();
        float vel= current.getMovingVelocity();
        float atk= current.getCurrentAtk();
        uiMgr.updateRtUi(hp,atk, vel);
    }

    public void updateTurnUi(int player, int curr, int count, string name)
    {
        uiMgr.updateTurnUi(turnNumber, player, curr, count, name);
    }

    public void switchTurn(bool forceSkip = false)
    {
        if (!isStart)
        {
            return;
        }

        Debug.Log("switch turn");

        if (turnNumber > 0)
        {
            if (current.getMovingVelocity() > 1.5 && !forceSkip)
            {
                Debug.Log("still too fast, vel:" + current.getMovingVelocity().ToString());
                return;
            }
            current.endTurn();
        }

        switch (turnIndex)
        {
            case 0:
                current = p1;
                break;
            case 1:
                current = p2;
                break;
        }

        turnNumber++;

        current.startTurn();

        turnIndex = (turnIndex + 1) % 2;
    }

    public void startGame()
    {
        foreach (Player p in p1.players)
        {
            p.gameObject.SetActive(true);
        }
        foreach (Player p in p2.players)
        {
            p.gameObject.SetActive(true);
        }
        p1.setTeam(0);
        p2.setTeam(1);
        setPreGameMenuVisible(false);
        setEndGameMenuVisible(false);
        setInGameMenuVisible(true);
        isStart = true;
        switchTurn();
    }

    public void endGame(int winner)
    {
        Debug.Log("winner " + winner.ToString());
        isStart = false;
        setEndGameMenuVisible(true);
        setInGameMenuVisible(false);
        endGameUi.win(winner + 1);
    }
    public void Update()
    {
        if (!isStart)
        {
            return;
        }
        else
        {
            if (!FindObjectOfType<AudioManager>().isPlaying("Background"))
            {
                FindObjectOfType<AudioManager>().Play("Background");
            }
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            
            switchTurn();
        }

        if (p1.players.Count == 0)
        {
            endGame(p2.team);
        }
        else if (p2.players.Count == 0) 
        {
            endGame(p1.team); 
        }
    }

    public void setPreGameMenuVisible(bool visible)
    {
        charSelectUi.gameObject.SetActive(visible);
    }

    public void setEndGameMenuVisible(bool visible)
    {
        endGameUi.gameObject.SetActive(visible);
    }

    public void setInGameMenuVisible(bool visible)
    {
        uiMgr.gameObject.SetActive(visible);
    }
}

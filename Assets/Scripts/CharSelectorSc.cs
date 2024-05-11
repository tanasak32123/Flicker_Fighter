using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectorSc : MonoBehaviour
{
    public GlobalManagerSc globalManagerSc;
    private int current = 0;

    void Start()
    {
        globalManagerSc = GetComponent<GlobalManagerSc>();
    }

    public void selectChar(Player charObject,Sprite image)
    {
        Player charPlayer;
        if (globalManagerSc.isStart) { return; }
        if (current == 0)
        {
            if (globalManagerSc.p1.players.Count >= 5) { return; }
            charPlayer = Instantiate(charObject, new Vector3(0, 0, 0), Quaternion.identity);
            globalManagerSc.p1.addPlayerChar(charPlayer,image);
            return;
        }
        if (globalManagerSc.p2.players.Count >= 5) { return; }
        charPlayer = Instantiate(charObject, new Vector3(0, 0, 0), Quaternion.identity);
        globalManagerSc.p2.addPlayerChar(charPlayer,image);
    }

    public void popBackChar()
    {
        if (globalManagerSc.isStart) { return; }
        if (current == 0)
        {
            globalManagerSc.p1.popPlayerChar();
            return;
        }
        globalManagerSc.p2.popPlayerChar();
    }

    public void nextSelect()
    {
        if (globalManagerSc.isStart) { return; }
        if (globalManagerSc.p1.players.Count < 1) { return; }
        Debug.Log("current:" + current);
        if (current == 1)
        {
            if (globalManagerSc.p2.players.Count < 1) { return; }
            Debug.Log("starting");
            globalManagerSc.startGame();
            return;
        }
        current++;
    }

    public List<string> getAllCharName(int player)
    {
        List<string> pl = new List<string>();
        if (player == 0)
        {
            foreach (Player p in globalManagerSc.p1.players)
            {
                pl.Add(p.ballName);
            }
        }
        else
        {
            foreach (Player p in globalManagerSc.p2.players)
            {
                pl.Add(p.ballName);
            }
        }
        return pl;
    }

    public List<Sprite> getAllCharSprite(int player)
    {
        List<Sprite> pl = new List<Sprite>();
        if (player == 0)
        {
            foreach (Sprite img in globalManagerSc.p1.playerSprites)
            {
                pl.Add(img);
            }
        }
        else
        {
            foreach (Sprite img in globalManagerSc.p2.playerSprites)
            {
                pl.Add(img);
            }
        }
        return pl;
    }

    public int getCurrent()
    {
        return current;
    }
}

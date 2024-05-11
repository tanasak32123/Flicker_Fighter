using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPU : PowerUp
{
    // Start is called before the first frame update
    private void Start()
    {
        pwName = "AtkUp";
        updateLabel();
    }

    protected override void handleCollisPlayer(Player player)
    {
        player.addAtkBuff(point);
    }
}

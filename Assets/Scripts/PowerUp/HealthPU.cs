using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class HealthPU : PowerUp
{
    private void Start()
    {
        pwName = "HEAL";
        check();
        updateLabel();
    }

    protected override void handleCollisPlayer(Player player)
    {
        player.SetHealth(player.health + point);
    }
}

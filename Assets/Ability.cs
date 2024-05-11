using System;

public enum AbilityStateEnum
{
    ready,
    active,
    cooldown
}

public class Ability
{
    public string AbilityName { get; set; }
    public int CooldownTurn { get; set; }
    public int ActiveTurn { get; set; }
    public AbilityStateEnum AbilityState { get; set; }

    public Ability(string abilityName, int cooldownTurn, int activeTurn)
    {
        AbilityName = abilityName;
        CooldownTurn = cooldownTurn;
        ActiveTurn = activeTurn;
        AbilityState = AbilityStateEnum.ready;
    }
}


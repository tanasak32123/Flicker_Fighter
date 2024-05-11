using System.Collections;
using System.Collections.Generic;

public class Character
{
    public string CharacterName { get; set; }
    public float currentHP { get; set; }
    public float HP { get; set; }
    public float Attack { get; set; }
    public float CurrentAttack { get; set; }
    public bool IsSelected { get; set; }
    public bool IsActivatedAbility { get; set; }
    public List<Ability> Abilities { get; set; }

    public Character(string name, float hp, float attack) 
    {
        CharacterName = name;
        HP = hp;
        currentHP = hp;
        Attack = attack;
        CurrentAttack = attack;
        IsSelected = false;
        IsActivatedAbility = false;
        Abilities = new List<Ability>();
    }

    public void AddNewCharacterAbility(Ability ability)
    {
        Abilities.Add(ability);
    }
}
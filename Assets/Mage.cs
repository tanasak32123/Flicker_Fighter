using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
	Character mage;

    void Start()
    {
        mage = new Character("Mage", 10, 2);

        Ability ability1 = new Ability("Fireball", 4, 2);

        mage.AddNewCharacterAbility(ability1);
    }

    void Update()
    {
           
    }
}


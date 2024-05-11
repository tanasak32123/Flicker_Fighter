using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    public float maxHealth = 10;
    public bool  isTurn = false;
    public float attack;
    public float critChance = 0;
    public float currentAttack;
    public bool moved = false;
    public int team;
    public string ballName = "default ball";
    public Rigidbody2D rb;
    SpriteRenderer m_SpriteRenderer;
    Color m_NewColor;

    public int abilityCount;
    public int RemainImmovabilityTurn { get; set; }
    public bool IsActivateAbility { get; set; }
    public bool IsUseAbilityOnTurn { get; set; }
    public bool IsMoving { get; set; }

    
    void Start()
    {
        health = maxHealth;
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Set the GameObject's Color quickly to a set Color (blue)
        if (team == 0)
        {
            m_SpriteRenderer.color = Color.blue;
        }
        else
        {
            m_SpriteRenderer.color = Color.red;
        }
        IsActivateAbility = false;
        IsUseAbilityOnTurn = false;
    }

    void Update() {
        if (rb.velocity.magnitude > 0) {
            IsMoving = true;
        }
        if (IsMoving && rb.velocity.magnitude <= 0) {
            IsMoving = false;
        }
    }

    public void setTeam(int team)
    {
        this.team = team;
        SetColorPlayerToDefault();
    }


    public void die()
    {
        // play dead animation

        // then destroy
         Destroy(gameObject);
    }

    public void DisplayAbilityButtons()
    {
        if (abilityCount <= 0) return;
        GameObject abilityButtons = gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        if (abilityButtons != null)
        {
            abilityButtons.SetActive(true);
        }
    }

    public void HideAbilityButtons()
    {
        if (abilityCount <= 0) return;
        GameObject abilityButtons = gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject;
        if (abilityButtons != null)
        {
            abilityButtons.SetActive(false);
        }
    }

    public void SetColorPlayerToDefault()
    {
        if (team == 0)
        {
            m_SpriteRenderer.color = Color.blue;
        }
        else
        {
            m_SpriteRenderer.color = Color.red;
        }
    }

    public void SetColorPlayer(Color color)
    {
        m_SpriteRenderer.color = color;
    }

    public void SetHealth(float health)
    {
        this.health = Mathf.Min(maxHealth, health);
    }

    public void addAtkBuff(float atkBuff)
    {
        if (atkBuff < 0)
        {
            return;
        }
        attack += atkBuff;
    }

    public void CooldownAllPlayerBuff() {
        
        CooldownImmobility();
    }

    private void CooldownImmobility() {
        if (RemainImmovabilityTurn > 0) {
            RemainImmovabilityTurn -= 1;
            
            if (RemainImmovabilityTurn == 0) {
                foreach (Transform child in transform) {
                    if (child.tag == "Chain") {
                        Destroy(child.gameObject);
                    }
                }

                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}

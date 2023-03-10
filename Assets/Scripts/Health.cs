using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public float healthPercent;
    public bool shieldActive;
    public float shieldDamage;
    public float shieldHeal;
    public Pawn owner;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthPercent = (currentHealth / maxHealth) * 100;
    }

    // Update is called once per frame
    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        if (shieldActive != true)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        } else
        {
            shieldDamage = shieldDamage + amount;
            if (shieldDamage >= shieldHeal)
            {
                ShieldDeactivate();
            }
        }
        if (source != null) 
        { 
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);
        }
        healthPercent = (currentHealth / maxHealth) * 100;
        Debug.Log("healthPercent of" + gameObject.name + " is " + healthPercent + "%");

        if (currentHealth <= 0)
        {
            Die(source);
        }
    }

    public void ClampHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthPercent = (currentHealth / maxHealth) * 100;
    }

    public void Heal(float amount)
    {
        currentHealth = currentHealth + amount;
        if (shieldActive != true)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
        Debug.Log(gameObject.name + " is healed by " + amount + "HP");
        healthPercent = (currentHealth / maxHealth) * 100;
        Debug.Log("healthPercent of " + gameObject.name + " is " + healthPercent + "%");
    }

    public bool IsHealthPercentBelow(float amount)
    {
        if (healthPercent <= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShieldActivate(float amount)
    {
        shieldActive = true;
        Heal(amount);
        shieldHeal = amount;
        shieldDamage = 0;
    }

    public void ShieldDeactivate()
    {
        shieldActive = false;
        if (shieldDamage < shieldHeal)
        {
            TakeDamage((shieldHeal - shieldDamage), owner);
        }
    }

    public void Die(Pawn source)
    {
        Debug.Log(source.name + " destroyed " + gameObject.name);
        Destroy(gameObject);
    }
}



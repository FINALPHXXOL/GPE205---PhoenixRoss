using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float currentHealth;
    public float maxHealth;
    public float healthPercent;
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
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);
        healthPercent = (currentHealth / maxHealth) * 100;
        Debug.Log("healthPercent of" + gameObject.name + " is " + healthPercent + "%");

        if (currentHealth <= 0)
        {
            Die(source);
        }
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

    public void Die(Pawn source)
    {
        Debug.Log(source.name + " destroyed " + gameObject.name);
        Destroy(gameObject);
    }
}



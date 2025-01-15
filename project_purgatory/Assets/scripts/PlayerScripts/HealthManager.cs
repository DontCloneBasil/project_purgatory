using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("references")]
    
    [SerializeField] public List<Resistances> resistances = new List<Resistances>
    {
        new Resistances("thunder", 1f),
        new Resistances("fire", 1f),
        new Resistances("dark", 1f),
        new Resistances("light", 1f),
        new Resistances("physical", 1f),
        new Resistances("environment", 1f)
    };
    [Header("stats")]
    // health modifier. supposed to be dynamic on the type of map
    public float modifier;
    public float baseHealth;
    [SerializeField] private float health;
    
    // Start is called before the first frame update
    void Start()
    {
        //collider = transform.GetComponent<Collider2D>();
        health = baseHealth * modifier; 
    }
    private void OnTriggerEnter2D (Collider2D attackObject)
    {
        // fetches the script containing the objects attack value
        var valueScript = attackObject.gameObject.GetComponent<AttackObjectValues>();
        // fetches the type of damage
        string T = valueScript.typeOfDamage;
        // fetches the damage value
        float DV = valueScript.DamageValue;
        AttackCalculations(T, DV);
    }
    //damage calculation method
    private void AttackCalculations(string AttackType,float BaseAttack)
    {
        foreach (var item in resistances)
        {
            // filters the type of damage
            if (item.name == AttackType)
            {
                // inflicts the damage
                health -= BaseAttack * item.damageModifier;
                // rounds it to 1 decimal
                health = Mathf.Round(health * 10.0f) * 0.1f;
                if (health <= 0f)
                {
                    Destroy(transform.gameObject);
                }
                return;
            }
        }
    }


    //makes the class visible in the editor
    [System.Serializable] 
    //class to handle resistances
    public class Resistances
{
    //name of the type of damage
    public string name;
    //the modifier which the type of damage is multiplied by.
    public float damageModifier;

    public Resistances(string name, float damageModifier)
    {
        this.name = name;
        this.damageModifier = damageModifier;
    }
}
}

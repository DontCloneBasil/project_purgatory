using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthManager : MonoBehaviour
{
    [Header("references")]
    public Slider healthSlider;
    public bool isPlayer;
    [SerializeField] private List<Resistances> resistances = new List<Resistances>
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
    [SerializeField] private float baseHealth;
    [SerializeField] private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth * modifier;

        //collider = transform.GetComponent<Collider2D>();
        if (transform.gameObject.layer == 7)
        {
            isPlayer = true;
            healthSlider.maxValue = health;
        }

    }

    void Update()
    {
        if(isPlayer)
        {
           if (healthSlider.value != health)
           {
                healthSlider.value = health;
           }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D attackObject)
    {
        if (attackObject.gameObject.layer == 6)
        {
            Debug.Log($"attacking object:{attackObject.gameObject.name} attacks: {transform.gameObject.name}");
            // fetches the script containing the objects attack value
            var valueScript = attackObject.gameObject.GetComponent<AttackObjectValues>();
            // fetches the type of damage
            string T = valueScript.typeOfDamage;
            // fetches the damage value
            float DV = valueScript.DamageValue;
            //checks if the hit object is or isn't the caster, at which point it doesn't do the damage calculation
            if(valueScript.caster.name != transform.gameObject.name)
            {
                AttackCalculations(T, DV);
            }
        }
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
                    // if the item is a player, will move the camera object away from the player before player gets deleted
                    if (isPlayer)
                    {
                        GameObject camera = transform.GetChild(0).gameObject;
                        camera.transform.parent = null;
                        BackToStart();
                        
                    }
                    Destroy(transform.gameObject);
                }
                return;
            }
        }
    }
    private void BackToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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

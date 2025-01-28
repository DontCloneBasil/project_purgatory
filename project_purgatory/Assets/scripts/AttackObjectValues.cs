using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObjectValues : MonoBehaviour
{
    public string typeOfDamage;
    public float DamageValue;
    public GameObject caster;
    
    void Start()
    {
        typeOfDamage = transform.tag;
    }
}

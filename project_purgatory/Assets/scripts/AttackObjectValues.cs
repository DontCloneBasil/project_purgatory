using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObjectValues : MonoBehaviour
{
    public string typeOfDamage;
    public float DamageValue;
    
    void Start()
    {
        typeOfDamage = transform.tag;
    }
}

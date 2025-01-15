using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public GameObject attackObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnAttack()
    {
        GameObject obj = new GameObject("NewGameObjectsName!");
        obj.AddComponent<Light>();
        obj.transform.position = new Vector2(10, 0);
        Debug.Log("pressed attack");
    }
}

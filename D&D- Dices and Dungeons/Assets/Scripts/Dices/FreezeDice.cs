using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FreezeDice : Dice
{
    [SerializeField] private GameObject freezePrefab;
    [SerializeField]private float freezeDuration;
    private float groundLevel = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        Random rg = new Random();
        freezeDuration = rg.Next(maxRoll) + 1;
    }

   
    
    
    protected override void OnCollisionEnter(Collision collision)
    {
        SpawnFreeze();
        DiceBehaviour();
    }

    void SpawnFreeze()
    {
        GameObject o = Instantiate(freezePrefab, new Vector3(transform.position.x, groundLevel, transform.position.z),
            Quaternion.identity);

        o.GetComponent<Freeze>().SetDuration = freezeDuration;


    }
}

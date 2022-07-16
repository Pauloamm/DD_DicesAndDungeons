using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StickyDice : Dice
{
    [SerializeField] private GameObject goopPrefab;
    [SerializeField]private float goopDuration;
    private float groundLevel = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        Random rg = new Random();
        goopDuration = rg.Next(maxRoll) + 1;
    }

   
    
    
    protected override void OnCollisionEnter(Collision collision)
    {
        SpawnGoop();
        DiceBehaviour();
    }

    void SpawnGoop()
    {
        GameObject o = Instantiate(goopPrefab, new Vector3(transform.position.x, groundLevel, transform.position.z),
            Quaternion.identity);

        o.GetComponent<Goop>().SetDuration = goopDuration;


    }
}

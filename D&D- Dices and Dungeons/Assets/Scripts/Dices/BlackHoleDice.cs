using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class BlackHoleDice : Dice
{
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField]private float blackHoleDuration;
    private float groundLevel = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        Random rg = new Random();
        blackHoleDuration = rg.Next(maxRoll) + 1;
    }

   
    
    
    protected override void OnCollisionEnter(Collision collision)
    {
        SpawnBlackHole();
        DiceBehaviour();
    }

    void SpawnBlackHole()
    {
        GameObject o = Instantiate(blackHolePrefab, new Vector3(transform.position.x, groundLevel, transform.position.z),
            Quaternion.identity);

        o.GetComponent<BlackHole>().SetDuration = blackHoleDuration;
    }
   
}

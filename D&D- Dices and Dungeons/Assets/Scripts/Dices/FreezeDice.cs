﻿using System.Collections;
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
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.tag.Equals("Ground"))
            {
                groundLevel = hit.point.y;
            }
        }

        GameObject o = Instantiate(freezePrefab, new Vector3(transform.position.x, groundLevel, transform.position.z),
            Quaternion.identity);

        o.GetComponent<Freeze>().SetDuration = freezeDuration;


    }
}

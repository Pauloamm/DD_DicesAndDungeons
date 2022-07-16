using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDice : Dice
{
    protected void Start()
    {
        damage = 0;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        GameObject player = GameObject.Find("Player");

        player.transform.position =
            new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        DiceBehaviour();

    }
}

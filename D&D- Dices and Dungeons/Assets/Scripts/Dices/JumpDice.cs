using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class JumpDice : Dice
{
    [SerializeField]
    private int knockbackForce;

    protected override void Update()
    {
        base.Update();

        if (this.transform.parent != null) return;
        JumpBoost();
        base.DiceBehaviour();
    }

    // protected override void OnCollisionEnter(Collision collision)
    // {
    //     JumpBoost();
    //     base.DiceBehaviour();
    // }

    void JumpBoost()
    {
        Rigidbody playerRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        Transform cameraT = GameObject.Find("Main Camera").transform;
        playerRb.AddForce(-cameraT.transform.forward * knockbackForce * DiceEffectMultiplier, ForceMode.Impulse);
    }
}

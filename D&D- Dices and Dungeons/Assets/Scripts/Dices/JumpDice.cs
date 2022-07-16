using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class JumpDice : Dice
{
    private int jumpRoll;

    [SerializeField]private int jumpForce;
    // Start is called before the first frame update
    protected override void Start()
    {
        damage = 0;
        Random rg = new Random();
        jumpRoll = rg.Next(maxRoll) + 1;

    }

    private void Update()
    {
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
        playerRb.AddForce(-cameraT.transform.forward * jumpForce * jumpRoll,ForceMode.Impulse);
    }
}

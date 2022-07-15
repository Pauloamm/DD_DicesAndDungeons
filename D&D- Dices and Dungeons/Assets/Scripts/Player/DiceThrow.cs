using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class DiceThrow : MonoBehaviour
{
    
    //TESTE 

    [SerializeField] private GameObject testDice;
    [SerializeField] private float diceImpulseStrength = 10f;
    [SerializeField] private Transform diceHolderT;
    
    
    private bool canThrow = false;
    
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canThrow = true;
            Debug.Log("DISPARA CARALHO");
        }

        if (Input.GetKeyDown(KeyCode.P))
            Instantiate(testDice,diceHolderT);

    }

     void FixedUpdate()
    {
        if (canThrow)
        {
            ShootDice();
            canThrow = false;
        }
    }

    void ShootDice()
    {
        testDice.GetComponent<Collider>().enabled = true;
        Rigidbody rb = testDice.GetComponent<Rigidbody>();
        testDice.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(this.transform.forward * diceImpulseStrength,ForceMode.Impulse);

        canThrow = false;
    }
}

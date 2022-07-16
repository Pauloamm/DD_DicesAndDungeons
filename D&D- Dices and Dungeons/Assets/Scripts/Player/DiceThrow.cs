using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class DiceThrow : MonoBehaviour
{
    
    //TESTE 
    [SerializeField] private DicesInventory dicesInventory;
    private GameObject dice => dicesInventory.GetCurrentDiceInHand;
    [SerializeField] private Transform diceHolderT;
    
    
    private bool canThrow = false;

    public delegate void DiceAction();

    public event DiceAction diceThrown;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canThrow = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
            Instantiate(dice,diceHolderT);

    }

     void FixedUpdate()
    {
        if (canThrow)
        {   
            dice.GetComponent<Dice>().Shoot();
            diceThrown?.Invoke();
            canThrow = false;
        }
    }

}

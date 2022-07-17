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

    private float throwCooldown = 1.0f;
    private float timeToNextThrow;

    public delegate void DiceAction();

    public event DiceAction diceThrown;

    void Update()
    {
        if (!PlayerTransitionController.Instance.PlayerReady)
            return;

        if (Input.GetMouseButtonDown(0) && timeToNextThrow <= 0)
        {
            canThrow = true;
        }
        else
        {
            timeToNextThrow -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (canThrow)
        {
            GetComponentInChildren<Animator>().SetTrigger("Attack");

            dice.GetComponent<Dice>().Shoot(transform.forward);
            diceThrown?.Invoke();
            timeToNextThrow = throwCooldown;
            canThrow = false;
        }
    }
}

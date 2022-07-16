﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DicesInventory : MonoBehaviour
{

    [SerializeField] private Transform diceHolder;
    [SerializeField]private List<GameObject> inventory;

    [SerializeField] private DiceThrow diceThrow;
    
    // Start is called before the first frame update

    //Prefabs references
    private GameObject currentDice;
    private GameObject nextDice;
    
    //Hand Instance
    private GameObject diceInHand;
    public GameObject GetCurrentDiceInHand => diceInHand;

    private bool canGrabNext = false;

    // Update is called once per frame


    private void Start()
    {
        diceThrow.diceThrown += GetNewDice;
        
        
        Random rg = new Random();
        int firstDice = rg.Next(inventory.Count);
        currentDice = inventory[firstDice];
        SpawnDice(currentDice);

        nextDice = inventory[rg.Next(inventory.Count)];
    }

    void Update()
    {
        if (!PlayerTransitionController.Instance.PlayerReady)
            return;

        RotateDice();
    }
    
    
    void RotateDice()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            GameObject temp = currentDice;

            currentDice = nextDice;
            
            nextDice = temp;
            
            DespawnDice(diceInHand);
            SpawnDice(currentDice);
            
        }
    }

    void GetNewDice()
    {
        currentDice = nextDice;
        
        Random rg = new Random();
        nextDice = inventory[rg.Next(inventory.Count)];
        SpawnDice(currentDice);
    }

    void SpawnDice(GameObject dicePrefab)
    {
        diceInHand = Instantiate(dicePrefab,diceHolder);
    }

    void DespawnDice(GameObject diceInstance)
    {
        DestroyImmediate(diceInstance);
    }
}
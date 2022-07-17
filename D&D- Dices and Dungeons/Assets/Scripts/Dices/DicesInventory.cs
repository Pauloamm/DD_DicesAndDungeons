using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class DicesInventory : MonoBehaviour
{
    [SerializeField]
    Image currentDiceSprite;
    [SerializeField]
    Image storedDiceSprite;

    [SerializeField] private Transform diceHolder;
    [SerializeField] private List<GameObject> inventory;

    [SerializeField] private DiceThrow diceThrow;

    //Prefabs references
    private GameObject currentDice;
    private GameObject nextDice;

    //Hand Instance
    private GameObject diceInHand;
    public GameObject GetCurrentDiceInHand => diceInHand;

    private bool canGrabNext = false;

    private void Start()
    {
        diceThrow.diceThrown += GetNewDice;

        Random rg = new Random();
        int firstDice = rg.Next(inventory.Count);
        currentDice = inventory[firstDice];
        SpawnDice(currentDice);

        nextDice = inventory[rg.Next(inventory.Count)];

        UpdateInventorySprites();
    }

    void Update()
    {
        if (!PlayerTransitionController.Instance.PlayerReady)
            return;

        SwitchDice();
    }


    void SwitchDice()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject temp = currentDice;

            currentDice = nextDice;

            nextDice = temp;

            DespawnDice(diceInHand);
            SpawnDice(currentDice);

            UpdateInventorySprites();
        }
    }

    void GetNewDice()
    {
        currentDice = nextDice;

        Random rg = new Random();
        nextDice = inventory[rg.Next(inventory.Count)];
        SpawnDice(currentDice);

        UpdateInventorySprites();
    }

    void SpawnDice(GameObject dicePrefab)
    {
        diceInHand = Instantiate(dicePrefab, diceHolder);

        // if (dicePrefab.GetComponent<RandomDice>() != null)
        // {
        //     GameObject temp;
        //     do
        //     {
        //         Random rg = new Random();
        //         temp = inventory[rg.Next(inventory.Count)];
        //
        //     } while (temp.GetComponent<RandomDice>() != null);
        //
        //
        //
        //     if (temp.GetComponent<NormalDice>() != null)
        //     {   
        //         NormalDice t = Instantiate()
        //         diceInHand.AddComponent<NormalDice>();
        //
        //     }
        //     
        //     if (temp.GetComponent<BombDice>() != null)
        //         diceInHand.AddComponent<BombDice>();
        //     
        //     if (temp.GetComponent<StickyDice>() != null)
        //         diceInHand.AddComponent<StickyDice>();
        //     
        //     if (temp.GetComponent<FreezeDice>() != null)
        //         diceInHand.AddComponent<FreezeDice>();
        //     
        //     if (temp.GetComponent<BlackHoleDice>() != null)
        //         diceInHand.AddComponent<BlackHoleDice>();
        //     
        //     if (temp.GetComponent<TeleportDice>() != null)
        //         diceInHand.AddComponent<TeleportDice>();
        //     
        //     if (temp.GetComponent<JumpDice>() != null)
        //         diceInHand.AddComponent<JumpDice>();
        // }


    }

    void DespawnDice(GameObject diceInstance)
    {
        DestroyImmediate(diceInstance);
    }

    void UpdateInventorySprites()
    {
        currentDiceSprite.sprite = currentDice.GetComponent<Dice>().diceSprite;
        storedDiceSprite.sprite = nextDice.GetComponent<Dice>().diceSprite;
    }
}
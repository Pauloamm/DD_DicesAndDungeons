using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DicesInventory : MonoBehaviour
{
    private List<GameObject> inventory;
    // Start is called before the first frame update


    private GameObject currentDice;
    private GameObject nextDice;


    private bool canGrabNext = false;

    // Update is called once per frame
    void Update()
    {
        // if (transform.childCount != 1) return;
        //
        //
        // currentDice = nextDice;
        // GameObject o = Instantiate(currentDice,this.transform);
        // currentDice.GetComponent<Dice>().ThisObject = o;
        //
        // Random rg = new Random();
        // int x = rg.Next(inventory.Count - 1);
        // nextDice = inventory[x];
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class NormalDice : Dice
{
    private int maxRoll = 6;
    
    // Start is called before the first frame update
    void Start()
    {
        Random rg = new Random();
        damage = rg.Next(maxRoll) + 1;
    }

}

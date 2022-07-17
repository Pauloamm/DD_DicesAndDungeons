using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TESTDICE : Dice
{
    private int maxRoll = 6;
    
    // Start is called before the first frame update
    void Start()
    {
        Random rg = new Random();
        DiceEffectMultiplier = rg.Next(maxRoll) + 1 ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

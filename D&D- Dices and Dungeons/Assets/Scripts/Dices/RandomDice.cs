using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomDice : Dice
{
    private int randomChance;

    protected override void Start()
    {
        base.Start();

        int randomDiceRoll = 100;

        Random rg = new Random();
        randomChance = rg.Next(randomDiceRoll);

        if (randomChance < 1)
        {
            PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            player.TakeDamage(3);
        }
        else if (randomChance < 33) DiceEffectMultiplier = 10;
        else if (randomChance < 66) DiceEffectMultiplier = 0;
        else if (randomChance < 100) DiceEffectMultiplier = 100;
    }
}

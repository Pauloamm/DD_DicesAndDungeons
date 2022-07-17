using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomDice : Dice
{
   private int maxRoll = 100;
   private int randomChance;

  
   
   protected override void Start()
   {
      Random rg = new Random();
      randomChance = rg.Next(maxRoll);

      if (randomChance < 1)
      {
         PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
         player.TakeDamage();
         player.TakeDamage();
         player.TakeDamage();

      }
      else if (randomChance < 33) damage = 10;
      else if (randomChance < 66) damage = 0;
      else if (randomChance < 100) damage = 100;

   }
}

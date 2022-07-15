using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy : MonoBehaviour,IHittable
{
    [SerializeField] private int hp;
    
    public void OnDiceHit(Dice dice)
    {
        hp -= dice.Damage;
    }
    
    
    
  

    // Update is called once per frame
    void Update()
    {
        if (hp > 0) return;

        Die();
    }

    void Die()
    {
        DestroyImmediate(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Dice : MonoBehaviour
{
    [SerializeField]protected int damage;

    public int Damage => damage;

    private GameObject thisObject;

    public GameObject ThisObject
    {
        set
        {
            thisObject = value;
        } 
    }
    
    protected int maxRoll = 6;

    protected virtual void Start()
    {
        Random rg = new Random();
        damage = rg.Next(maxRoll) + 1;
    }
    
    protected virtual void OnCollisionEnter(Collision collision)
    {
        IHittable temp = collision.gameObject.GetComponent<IHittable>();

        if (temp != null)
        {
            temp.OnDiceHit(this);
            DiceBehaviour();
        }
        
        
    }


    protected virtual void DiceBehaviour() 
    {
        Destroy(this.gameObject);
    }
}

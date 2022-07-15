using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BombDice : Dice
{

    private int maxRoll = 6;

    [SerializeField] private int explosionRadius;

    [SerializeField]private ParticleSystem explosionParticlesPrefab;
   
    void Start()
    {
        Random rg = new Random();
        damage = rg.Next(maxRoll) + 1;
    }
    
    protected override void OnCollisionEnter(Collision collision)
    {
        Explosion();
        Debug.Log("IT HIT");
    }


    void Explosion()
    {
        Collider[] collidersHit = Physics.OverlapSphere(this.transform.position, explosionRadius);

        foreach (Collider c in collidersHit)
        {
            IHittable temp = c.GetComponent<IHittable>();

            if (temp != null)
            {
                temp.OnDiceHit(this);
            }
            

        }
        
        DiceBehaviour();

    }

    protected override void DiceBehaviour()
    {
        Instantiate(explosionParticlesPrefab,transform.position,Quaternion.identity);
        
        base.DiceBehaviour();
    }
}

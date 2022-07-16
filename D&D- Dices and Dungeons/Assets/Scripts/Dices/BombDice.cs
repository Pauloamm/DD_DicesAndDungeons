using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDice : Dice
{


    [SerializeField] private int explosionRadius;

    [SerializeField]private ParticleSystem explosionParticlesPrefab;
   
   
    
    protected override void OnCollisionEnter(Collision collision)
    {
        Explosion();
        DiceBehaviour();

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
        

    }

    protected override void DiceBehaviour()
    {
        Instantiate(explosionParticlesPrefab,transform.position,Quaternion.identity);
        
        base.DiceBehaviour();
    }
}

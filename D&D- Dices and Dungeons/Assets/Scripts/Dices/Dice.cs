using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Dice : FloatingItem
{
    [SerializeField] protected int damage;

    [SerializeField] private float diceImpulseStrength = 10f;
    public int Damage => damage;


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
        }

        DiceBehaviour();
    }

    public virtual void Shoot(Vector3 throwDirection)
    {
        this.GetComponent<Collider>().enabled = true;
        Rigidbody rb = this.GetComponent<Rigidbody>();
        this.transform.parent = null;
        rb.isKinematic = false;
        rb.AddForce(throwDirection * diceImpulseStrength, ForceMode.Impulse);
    }

    protected virtual void DiceBehaviour()
    {
        Destroy(this.gameObject);
    }
}
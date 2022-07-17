using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Dice : FloatingItem
{
    [SerializeField]
    public Sprite diceSprite;

    [SerializeField]
    public int DiceEffectMultiplier { get; protected set; }

    private float diceImpulseStrength = 20f;

    private int maxRoll = 6;

    private Vector3[] _diceNewUpVector;

    protected virtual void Start()
    {
        Random rg = new Random();
        DiceEffectMultiplier = rg.Next(maxRoll) + 1;

        //_diceNewUpVector = new Vector3[6];
        //_diceNewUpVector[0] = new Vector3(0, 1, 0);
        //_diceNewUpVector[1] = new Vector3(0, 0, -1);
        //_diceNewUpVector[2] = new Vector3(-1, 0, 0);
        //_diceNewUpVector[3] = new Vector3(1, 0, 0);
        //_diceNewUpVector[4] = new Vector3(0, 0, 1);
        //_diceNewUpVector[5] = new Vector3(0, -1, 0);

        Sprite diceNumberSprite = Resources.Load<Sprite>("GameUI/DiceNumbers/" + DiceEffectMultiplier);
        GameObject.Find("DiceNumber").GetComponent<Image>().sprite = diceNumberSprite;
    }

    protected override void Update()
    {
        base.Update();
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
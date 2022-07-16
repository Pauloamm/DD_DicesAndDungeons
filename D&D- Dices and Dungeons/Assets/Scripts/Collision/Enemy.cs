using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IHittable
{
    [SerializeField] private int hp;
    [SerializeField] private float speed;

    public float SetSpeed
    {
        set { speed = value; }
    }

    private float defaultSpeed;

    private Transform player;


    private float separationRadius = 2f;

    private Vector3 velocityModifier;

    public Vector3 SetVelocityModifier
    {
        set { velocityModifier = value; }
    }

    public void OnDiceHit(Dice dice)
    {
        hp -= dice.Damage;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        defaultSpeed = speed;
    }


    // Update is called once per frame
    void Update()
    {
        if (hp < 0) Die();
    }

    private void LateUpdate()
    {
        Move();
        speed = defaultSpeed;
        velocityModifier = Vector3.zero;
    }

    void Move()
    {
        Vector3 direction = Separation() + Follow() + velocityModifier;

        this.transform.position += direction * (speed * Time.deltaTime);
    }

    Vector3 Separation()
    {
        Vector3 velocity = Vector3.zero;

        Collider[] collidersHit = Physics.OverlapSphere(transform.position, separationRadius);


        foreach (Collider hit in collidersHit)
        {
            if (hit.gameObject.transform.position != transform.position && hit.gameObject.CompareTag("Enemy"))
            {
                velocity += transform.position - hit.transform.position;
                velocity.y *= 0f;
            }
        }

        return velocity;
    }

    Vector3 Follow()
    {
        Vector3 convertOriginTo2D = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 convertTargetTo2D = new Vector3(player.position.x, 0f, player.position.z);

        Vector3 direction = convertTargetTo2D - convertOriginTo2D;


        return direction;
    }

    void Die()
    {
        DestroyImmediate(this.gameObject);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IHittable
{
    private float attackCoolDown = 2.0f;
    private float timeToNextAttack = 0;

    [SerializeField] private int hp;

    public int SetHP
    {
        set { hp = value; }
    }

    [SerializeField] private float speed;
    [SerializeField] private AudioClip hitSFX;

    public float SetSpeed
    {
        set { speed = value; }
    }

    private float defaultSpeed;

    private Transform player;


    private float separationRadius = 2f;

    private Vector3 velocityModifier;

    private float minDistToAttack = 1.5f;

    public Vector3 SetVelocityModifier
    {
        set { velocityModifier = value; }
    }

    public void OnDiceHit(Dice dice)
    {
        hp -= dice.DiceEffectMultiplier;
        GameObject.Find("EnemyHitSoundPlayer").GetComponent<AudioSource>().PlayOneShot(hitSFX);
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
        Debug.Log(timeToNextAttack);
        if (timeToNextAttack > 0)
        {
            timeToNextAttack -= Time.deltaTime;
            speed = defaultSpeed * 0.5f;
        }
        else
        {
            TryToAttack();
        }

        Move();
        speed = defaultSpeed;
        velocityModifier = Vector3.zero;
    }

    void Move()
    {
        Vector3 dirToPlayer = Follow().normalized ;

        Vector3 direction = Separation() + (dirToPlayer * speed) + velocityModifier;


        this.transform.position += direction * (speed * Time.deltaTime);

        this.transform.forward = dirToPlayer;
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
        GameObject.Find("WaveMananger").GetComponent<WaveManager>().currentEnemies.Remove(this.gameObject);

        DestroyImmediate(this.gameObject);
    }

    void TryToAttack()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
        if (distance <= minDistToAttack)
        {
            // Attack
            player.GetComponent<PlayerMovement>().TakeDamage(1);

            GetComponent<Animator>().SetTrigger("Attack");

            Debug.Log("Reset timer");
            timeToNextAttack = attackCoolDown;
        }
    }
}
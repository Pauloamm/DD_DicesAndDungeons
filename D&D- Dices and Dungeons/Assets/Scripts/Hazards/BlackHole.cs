using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private float rotationStrength;
    private float duration;

    public float SetDuration
    {
        set
        {
            duration = value;
        }
    }
    private float timer;

    private float radius = 6f;
    private float strength = 2f;
    
    // Start is called before the first frame update
    void Awake()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= duration)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        
        
        timer += Time.deltaTime;
        transform.Rotate(transform.up,rotationStrength);
        CheckEnemiesOnBlackHole();
    }

    void CheckEnemiesOnBlackHole()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in collidersHit)
        {
            if ( hit.gameObject.CompareTag("Enemy"))
            {
                Vector3 directionToPull = this.transform.position - hit.transform.position;
                directionToPull.y *= 0;
                hit.gameObject.GetComponent<Enemy>().SetVelocityModifier = directionToPull*strength;
            }
        }
    }
}

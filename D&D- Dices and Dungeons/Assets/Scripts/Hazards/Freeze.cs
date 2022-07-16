using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour
{
     
    private float duration;

    public float SetDuration
    {
        set
        {
            duration = value;
        }
    }
    private float timer;

    private float radius = 3f;
    
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
        
        CheckEnemiesOnFreeze();
    }





    void CheckEnemiesOnFreeze()
    {
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, radius);


        foreach (Collider hit in collidersHit)
        {
            if ( hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<Enemy>().SetSpeed = 0;
            }


        }
    }
}

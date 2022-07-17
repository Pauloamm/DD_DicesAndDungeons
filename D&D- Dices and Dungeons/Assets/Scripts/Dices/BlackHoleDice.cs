using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class BlackHoleDice : Dice
{
    [SerializeField] private GameObject blackHolePrefab;
    [SerializeField] private float blackHoleDuration;
    private float groundLevel = 0f;

    protected override void Start()
    {
        base.Start();

        blackHoleDuration = DiceEffectMultiplier;

        DiceEffectMultiplier = 0;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        SpawnBlackHole();
        DiceBehaviour();
    }

    void SpawnBlackHole()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.tag.Equals("Ground"))
            {
                groundLevel = hit.point.y;
            }
        }

        GameObject o = Instantiate(blackHolePrefab, new Vector3(transform.position.x, groundLevel, transform.position.z),
            Quaternion.identity);

        o.GetComponent<BlackHole>().SetDuration = blackHoleDuration;
    }
}

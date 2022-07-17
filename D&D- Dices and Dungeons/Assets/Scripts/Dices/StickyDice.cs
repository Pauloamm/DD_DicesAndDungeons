using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StickyDice : Dice
{
    [SerializeField] private GameObject goopPrefab;
    [SerializeField] private float stickyDuration;
    private float groundLevel = 0f;

    protected override void Start()
    {
        base.Start();

        stickyDuration = DiceEffectMultiplier;

        DiceEffectMultiplier = 0;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        SpawnGoop();
        DiceBehaviour();
    }

    void SpawnGoop()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.tag.Equals("Ground"))
            {
                groundLevel = hit.point.y;
            }
        }

        GameObject o = Instantiate(goopPrefab, new Vector3(transform.position.x, groundLevel, transform.position.z),
            Quaternion.identity);

        o.GetComponent<Goop>().SetDuration = stickyDuration;
    }
}

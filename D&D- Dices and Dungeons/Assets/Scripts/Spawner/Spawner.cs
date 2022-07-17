using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject poofSmokePrefab;

    [SerializeField] private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // SpawnEnemy();
        // PoofEffect();
    }

    private float time = 2f;
    private float timer = 0f;

    // Update is called once per frame
    // void Update()
    // {
    //     if (timer >= time)
    //     {
    //         Spawn();
    //         timer = 0f;
    //     }
    //     else timer += Time.deltaTime;
    // }

    public GameObject Spawn(int enemyHP)
    {
        GameObject newEnemy = SpawnEnemy(enemyHP);
        PoofEffect();

        return newEnemy;
    }

    public GameObject SpawnEnemy(int enemyHP)
    {
        Enemy e = Instantiate(enemyPrefab, this.transform.position, Quaternion.identity, null).GetComponent<Enemy>();
        e.SetHP = enemyHP;

        return e.gameObject;
    }

    void PoofEffect() => Instantiate(poofSmokePrefab, this.transform.position, Quaternion.identity, null);
}
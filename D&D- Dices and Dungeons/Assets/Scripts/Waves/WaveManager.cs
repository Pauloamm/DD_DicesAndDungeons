using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class WaveManager : MonoBehaviour
{
    struct WaveValues
    {
        public int numberWave;
        public int numberEnemies;
        public int minEnemyyHP, maxEnemyHP;
        public int activeSpawners;
        public int maxTimeBetweenSpawn;
    };

    private int baseWaveEnemies = 6;
    private int baseWaveSpawners = 1;

    private WaveValues firstWave = new WaveValues()
    {
        numberEnemies = 6,
        minEnemyyHP = 1,
        maxEnemyHP = 6,
        activeSpawners = 1,
        maxTimeBetweenSpawn = 2
    };

    private WaveValues secondWave = new WaveValues()
    {
        numberEnemies = 12,
        minEnemyyHP = 3,
        maxEnemyHP = 9,
        activeSpawners = 2,
        maxTimeBetweenSpawn = 3
    };

    private WaveValues thirdWave = new WaveValues()
    {
        numberEnemies = 18,
        minEnemyyHP = 6,
        maxEnemyHP = 12,
        activeSpawners = 3,
        maxTimeBetweenSpawn = 3
    };

    private WaveValues fourthWave = new WaveValues()
    {
        numberEnemies = 24,
        minEnemyyHP = 6,
        maxEnemyHP = 16,
        activeSpawners = 4,
        maxTimeBetweenSpawn = 3
    };

    private WaveValues fifthWave = new WaveValues()
    {
        numberEnemies = 30,
        minEnemyyHP = 12,
        maxEnemyHP = 21,
        activeSpawners = 5,
        maxTimeBetweenSpawn = 4
    };

    private WaveValues sixthWave = new WaveValues()
    {
        numberEnemies = 50,
        minEnemyyHP = 21,
        maxEnemyHP = 30,
        activeSpawners = 6,
        maxTimeBetweenSpawn = 4
    };


    private Queue<WaveValues> waves;
    private WaveValues currentWave;

    [SerializeField]private static int remainingEnemies;
    
    [SerializeField] private List<GameObject> spawners;
    private List<GameObject> currentActiveSpawners;

    
    // Start is called before the first frame update
    void Start()
    {
        waves = new Queue<WaveValues>();
        waves.Enqueue(firstWave);
        waves.Enqueue(secondWave);
        waves.Enqueue(thirdWave);
        waves.Enqueue(fourthWave);
        waves.Enqueue(fifthWave);
        waves.Enqueue(sixthWave);

        GetNextWave();
    }

    void GetNextWave()
    {
        currentWave = waves.Dequeue();
        remainingEnemies = currentWave.numberEnemies;
        timeUntilNextSpawn = 0;
        enemiesToSpawn = currentWave.numberEnemies;
    }

    [SerializeField]private float timeUntilNextSpawn;
    [SerializeField] private float enemiesToSpawn;

    void Update()
    {
        if (enemiesToSpawn > 0)
        {
            if (timeUntilNextSpawn <= 0)
            {
                SpawnEnemyInRandomSpawner();

                Random rg = new Random();
                timeUntilNextSpawn = rg.Next(currentWave.maxTimeBetweenSpawn) + 1;
                enemiesToSpawn--;
                return;
            }

            timeUntilNextSpawn -= Time.deltaTime;
            return;
        }
        
        //GetNextWave();
    }


    void SpawnEnemyInRandomSpawner()
    {
        Random rg = new Random();
        int spawnerToSelect = rg.Next(spawners.Count);
        int enemyHP = rg.Next(currentWave.minEnemyyHP,currentWave.maxEnemyHP) ;
        spawners[spawnerToSelect].GetComponent<Spawner>().Spawn(enemyHP);

    }
    static void LowerEnemiesRemaining()
    {
        remainingEnemies--;
    }
}
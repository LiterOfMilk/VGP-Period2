using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject powerfulEnemyPrefab;
    public GameObject miniPowerfulEnemyPrefab;
    public GameObject[] powerupPrefabs;
    private float spawnRange = 9;
    private int powerfulEnemyCountdown = 3;
    private int enemyCount;
    private int waveNumber = 1;
    private int randomIndex;
    private int miniPowerfulEnemyCountdown = 10;

    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

    // Start is called before the first frame update
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        SpawnEnemyWave(waveNumber);
    }

    void SpawnEnemyWave(int enemysToSpawn)
    {
        for(int i = 0; i < enemysToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], GenerateSpawnPosition(), enemyPrefab[randomEnemy].transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0)
        {
            waveNumber++;

            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);

            powerfulEnemyCountdown -= 1;
            if(powerfulEnemyCountdown == 0)
            {
                Instantiate(powerfulEnemyPrefab, GenerateSpawnPosition(), powerfulEnemyPrefab.transform.rotation);    
                powerfulEnemyCountdown = 3;
            }
        }
    }
    
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    
    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;

        if(bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }

        var boss =  Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;

    }

    public void SpawnMiniEnemy(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            miniPowerfulEnemyCountdown -= 1;
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
            if(miniPowerfulEnemyCountdown <= 0)
            {
                Instantiate(miniPowerfulEnemyPrefab, GenerateSpawnPosition(), miniPowerfulEnemyPrefab.transform.rotation);
                miniPowerfulEnemyCountdown = 10;
            }
        }
    }
}

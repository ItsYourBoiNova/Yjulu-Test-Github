using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int difficultyModifier, numberOfEnemiesPerSpawn;
    float spawnCD, spawnCDCounter, increaseDifficultyModifierCounter, enemiesSpeed;
    List<float> enemySpawnPoints = new List<float>();

    void Awake()
    {
        SetValuesFromDifficultyModifier();
        ResetEnemieSpawnPointsList();
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCountDown();
        IncreaseDifficultyModifier();
    }
    private void SpawnCountDown()
    {
        if (spawnCDCounter > 0)
        {
            spawnCDCounter -= Time.deltaTime;
        }
        else
        {
            spawnCDCounter = 0;
            SpawnWave();
        }
    }
    private void SpawnWave()
    {
        int SpecialEncounterChance = Random.Range(0, 100);
        if (SpecialEncounterChance > difficultyModifier)
        {
            SpawnNormalWave();

        }
        else
        {
            SpawnUniqueWave();
        }

        spawnCDCounter = spawnCD;
        ResetEnemieSpawnPointsList();
    }
    private void SpawnNormalWave()
    {
        for (int i = 0; i < numberOfEnemiesPerSpawn; i++)
        {
            int spawnPointz = Random.Range(0, enemySpawnPoints.Count);
            GameObject enemy =
      Instantiate(Resources.Load("Enemies/BasicEnemy"), new Vector3(transform.position.x, transform.position.y, enemySpawnPoints[spawnPointz]), Quaternion.identity) as GameObject;
            enemy.GetComponent<EnemyClassesParent>().speed = enemiesSpeed;
            enemySpawnPoints.Remove(enemySpawnPoints[spawnPointz]);

        }
        int spawnPointZCoin = Random.Range(0, enemySpawnPoints.Count);
       
        GameObject coin
    = Instantiate(Resources.Load("Bonuses/GoldCoin"), new Vector3(transform.position.x, transform.position.y, enemySpawnPoints[spawnPointZCoin]), Quaternion.identity) as GameObject;
        coin.GetComponent<EnemyClassesParent>().speed = enemiesSpeed;
    }

    private void SpawnUniqueWave()
    {
        GameObject uniqueEnemy =
 Instantiate(Resources.Load("Enemies/UniqueEnemy1"), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        uniqueEnemy.GetComponent<EnemyClassesParent>().speed = enemiesSpeed;
    }
    private void ResetEnemieSpawnPointsList()
    {
        //these values are hard coded based on the plane
        enemySpawnPoints = new float[] { -4, -2.5f, -1, 0.5f,2, 3.5f, 5 }.ToList();
    }
    private void IncreaseDifficultyModifier()
    {
        increaseDifficultyModifierCounter += Time.deltaTime;
        if (increaseDifficultyModifierCounter >= 15)
        {
            difficultyModifier++;
            SetValuesFromDifficultyModifier();
            increaseDifficultyModifierCounter = 0;
        }
    }

    private void SetValuesFromDifficultyModifier()
    {
        //max number of enemies = 6
       
        switch (difficultyModifier)
        {
            case 0:
                numberOfEnemiesPerSpawn = 4;
                spawnCD = 3f;
                enemiesSpeed = 10;
                break;
            case 1:
                numberOfEnemiesPerSpawn = 4;
                spawnCD = 2.5f;
                enemiesSpeed = 12;
                break;
            case 2:
                numberOfEnemiesPerSpawn = 5;
                spawnCD = 2.5f;
                enemiesSpeed = 15;
                break;
            case 3:
                numberOfEnemiesPerSpawn = 5;
                spawnCD = 2;
                enemiesSpeed = 17;
                break;
            case 4:
                numberOfEnemiesPerSpawn = 6;
                spawnCD = 2;
                enemiesSpeed = 20;
                break;
            case 5:
                numberOfEnemiesPerSpawn = 6;
                spawnCD = 1.5f;
                enemiesSpeed = 20;
                break;
            case 6:
                numberOfEnemiesPerSpawn = 6;
                spawnCD = 1.5f;
                enemiesSpeed = 22;
                break;
            case 7:
                numberOfEnemiesPerSpawn = 6;
                spawnCD = 1.5f;
                enemiesSpeed = 25;
                break;
            default:
                numberOfEnemiesPerSpawn = 6;
                spawnCD = 1.5f;
                enemiesSpeed = 5;
                break;
        }
    }
}

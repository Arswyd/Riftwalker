using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnTime = 10f;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject realm1;
    [SerializeField] GameObject realm2;

    EnemyDefenseHandler enemyDefenseHandler;
    bool isPrimaryRealm = true;
    float passedTime;
    List<int> availableSpawnPoints;
    EnemySpawnPoint[] allSpawnPoints;

    void Awake()
    {
        allSpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
        availableSpawnPoints = Enumerable.Range(0, allSpawnPoints.Length).ToList();
        enemyDefenseHandler = FindObjectOfType<EnemyDefenseHandler>();
    }

    void Update()
    {
        if (passedTime >= spawnTime)
        {
            PrepareEnemySpawn();
            passedTime = 0;
        }

        passedTime += Time.deltaTime;
    }

    void PrepareEnemySpawn()
    {
        if(availableSpawnPoints.Count > 0)
        {
            int index = Random.Range(0, availableSpawnPoints.Count);
            SpawnEnemy(realm1, availableSpawnPoints[index], isPrimaryRealm);
            SpawnEnemy(realm2, availableSpawnPoints[index], !isPrimaryRealm);
            availableSpawnPoints.RemoveAt(index);
        }
    }

    void SpawnEnemy(GameObject realm, int index, bool setActive)
    {
        var newEnemy = Instantiate(enemy, allSpawnPoints[index].transform.position, allSpawnPoints[index].transform.rotation);
        newEnemy.transform.parent = realm.transform;
        newEnemy.SetActive(setActive);
    }

    public void SwitchRealms()
    {
        foreach(Transform child in realm2.transform)
        {
            child.gameObject.SetActive(isPrimaryRealm);
        }
        foreach(Transform child in realm1.transform)
        {
            child.gameObject.SetActive(!isPrimaryRealm);
        }

        isPrimaryRealm = !isPrimaryRealm;
        enemyDefenseHandler.ResetDefenseLevel();

        availableSpawnPoints = Enumerable.Range(0, allSpawnPoints.Length).ToList();
    }
}

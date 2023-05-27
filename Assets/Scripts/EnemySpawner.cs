using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering.PostProcessing;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float minSpawnTime = 10f;
    [SerializeField] float maxSpawnTime = 10f;
    [SerializeField] float spawnTimeDecrament = 0.1f;
    [SerializeField] float decramentTime = 1f;
    [SerializeField] float spawnTimeIncrement = 2f;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject realm1;
    [SerializeField] GameObject realm2;
    [SerializeField] GameObject spawnEffect;
    [SerializeField] bool spawnTimeDebug;

    bool isPrimaryRealm = true;
    float passedTimeBetweenSpawns;
    float passedTimeBetweenDecraments;
    EnemySpawnPoint[] allSpawnPoints;
    PostProcessVolume volume;
    float spawnTime;
    int lastIndex;

    void Awake()
    {
        allSpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
        volume = FindObjectOfType<PostProcessVolume>();
        spawnTime = maxSpawnTime;
    }

    void Update()
    {
        passedTimeBetweenSpawns += Time.deltaTime;
        passedTimeBetweenDecraments += Time.deltaTime;

        if (passedTimeBetweenSpawns >= spawnTime)
        {
            PrepareEnemySpawn();
            passedTimeBetweenSpawns = 0;
        }

        if (passedTimeBetweenDecraments >= decramentTime)
        {
            spawnTime = Mathf.Max(minSpawnTime, spawnTime - spawnTimeDecrament);
            if(spawnTimeDebug)
                Debug.Log(spawnTime);
            passedTimeBetweenDecraments = 0;
        }

        if (isPrimaryRealm && volume.weight >= 0)
        {
            volume.weight -= Time.deltaTime;
        }
        else if (!isPrimaryRealm && volume.weight <= 1)
        {
            volume.weight += Time.deltaTime;
        }
    }

    void PrepareEnemySpawn()
    {
        int index;

        do
        {
            index = Random.Range(0, allSpawnPoints.Length);
        }
        while(index == lastIndex);

        lastIndex = index;

        StartCoroutine(StartSpawning(index, true));
    }

    void SpawnEnemy(GameObject realm, int index)
    {
        var newEnemy = Instantiate(enemy, allSpawnPoints[index].transform.position, allSpawnPoints[index].transform.rotation);
        newEnemy.transform.parent = realm.transform;
    }

    public void SwitchRealms()
    {
        realm2.SetActive(isPrimaryRealm);
        realm1.SetActive(!isPrimaryRealm);
        isPrimaryRealm = !isPrimaryRealm;

        spawnTime = Mathf.Min(maxSpawnTime, spawnTime + spawnTimeIncrement);
        if(spawnTimeDebug)
            Debug.Log(spawnTime);
        passedTimeBetweenDecraments = 0;
    }

    IEnumerator StartSpawning(int index, bool isOnlyCurrentRealm)
    {
        GameObject instance = Instantiate(spawnEffect, allSpawnPoints[index].transform.position, allSpawnPoints[index].transform.rotation);

        yield return new WaitForSeconds(1.5f);

        if(isPrimaryRealm)
        {
            SpawnEnemy(realm1, index);
        }
        else if (!isPrimaryRealm)
        {
            SpawnEnemy(realm2, index);
        }

        Destroy(instance);
    }
}

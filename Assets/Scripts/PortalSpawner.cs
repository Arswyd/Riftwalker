using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] float spawnTime = 10f;
    [SerializeField] GameObject portal;

    float passedTime;
    PortalSpawnPoint[] allSpawnPoints;
    bool isPortalSpawned;

    int lastIndex;

    void Awake()
    {
        allSpawnPoints = FindObjectsOfType<PortalSpawnPoint>();
        //Debug.Log(allSpawnPoints.Length);
    }

    void Update()
    {
        //Debug.Log(portal);
        if (passedTime >= spawnTime)
        {
            SpawnPortal();
            passedTime = 0;
        }

        passedTime += Time.deltaTime;
    }

    void SpawnPortal()
    {
        if (isPortalSpawned) {return;}

        int index;

        do
        {
            index = Random.Range(0, allSpawnPoints.Length);
        }
        while(index == lastIndex);

        lastIndex = index;

        Instantiate(portal, allSpawnPoints[index].transform.position, allSpawnPoints[index].transform.rotation);

        isPortalSpawned = true;
    }

    public void SetIsPortalSpawned(bool isSpawned)
    {
        isPortalSpawned = isSpawned; //Always false
        passedTime = 0;
    }
}

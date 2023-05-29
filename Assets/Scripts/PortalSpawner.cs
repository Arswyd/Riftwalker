using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] float spawnTime = 10f;
    [SerializeField] GameObject portal;

    float passedTime;
    PortalSpawnPoint[] allSpawnPoints;
    List<Vector2> weightedSpawnPointList = new List<Vector2>();
    bool isPortalSpawned;

    Vector2 lastPosition;

    void Awake()
    {
        allSpawnPoints = FindObjectsOfType<PortalSpawnPoint>();
        foreach(PortalSpawnPoint p in allSpawnPoints)
        {
            for(int i = 0; i < p.GetWeight(); i++)
            {
                weightedSpawnPointList.Add(p.transform.position);
            }
        }
    }

    void Update()
    {
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

        Vector2 position;

        do
        {
            position = weightedSpawnPointList[Random.Range(0, weightedSpawnPointList.Count)];
        }
        while(position == lastPosition);

        lastPosition = position;

        Instantiate(portal, position, transform.rotation);

        isPortalSpawned = true;
    }

    public void SetIsPortalSpawned(bool isSpawned)
    {
        isPortalSpawned = isSpawned; //Always false
        passedTime = 0;
    }
}

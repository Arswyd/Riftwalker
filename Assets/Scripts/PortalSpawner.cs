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

    int lastIndex;

    Vector2 lastPosition;

    void Awake()
    {
        allSpawnPoints = FindObjectsOfType<PortalSpawnPoint>();
        //Debug.Log(allSpawnPoints.Length);
        foreach(PortalSpawnPoint p in allSpawnPoints)
        {
            for(int i = 0; i < p.GetWeight(); i++)
            {
                weightedSpawnPointList.Add(p.transform.position);
                Debug.Log(p.GetWeight());
            }
        }
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

/*         int index;

        do
        {
            index = Random.Range(0, allSpawnPoints.Length);
        }
        while(index == lastIndex);

        lastIndex = index;

        Instantiate(portal, allSpawnPoints[index].transform.position, allSpawnPoints[index].transform.rotation); */

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

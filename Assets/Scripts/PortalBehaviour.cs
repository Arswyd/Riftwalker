using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    EnemySpawner enemySpawner;
    PortalSpawner portalSpawner;
    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        portalSpawner = FindObjectOfType<PortalSpawner>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemySpawner.SwitchRealms();
            portalSpawner.SetIsPortalSpawned(false);
            Destroy(gameObject);
        }
    }
}

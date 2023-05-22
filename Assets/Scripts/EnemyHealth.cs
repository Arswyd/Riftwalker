using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth = 100f;

    AIDestinationSetter aIDestinationSetter;
    EnemyDefenseHandler enemyDefenseHandler;

    float damageRecieved;

    void Awake()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aIDestinationSetter.target = FindObjectOfType<PlayerMovement>().transform;
        enemyDefenseHandler = FindObjectOfType<EnemyDefenseHandler>();
        damageRecieved = enemyHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Arrow")
        {
            enemyHealth -= damageRecieved * (1f / enemyDefenseHandler.GetDefenseLevel());
            //Debug.Log(enemyHealth);

            if (enemyHealth <= 0)
                Destroy(gameObject);
        }
    }
}

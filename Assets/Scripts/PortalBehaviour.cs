using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    [SerializeField] float activationTime = 2f;
    EnemySpawner enemySpawner;
    PortalSpawner portalSpawner;
    bool isActive;

    AudioManager audioManager;
    PlayerHealth playerHealth;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        portalSpawner = FindObjectOfType<PortalSpawner>();
        audioManager = FindObjectOfType<AudioManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Start()
    {
        StartCoroutine(Activation());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
        {
            playerHealth.SetInvincibility();
            audioManager.ModifySourcePitch();
            enemySpawner.SwitchRealms();
            portalSpawner.SetIsPortalSpawned(false);
            Destroy(gameObject);
        }
    }

    IEnumerator Activation()
    {
        yield return new WaitForSeconds(activationTime);

        isActive = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] AudioClip hitSFX;

    PlayerHealth playerHealth;

    bool isDestroyed;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isDestroyed) {return;}

        isDestroyed = true;
        Destroy(gameObject);

        AudioSource.PlayClipAtPoint(hitSFX, transform.position, 0.5f);

        if(other.tag == "Wall" || other.tag =="Obstacle")
        {
            GameObject instance = Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(instance, 1f);
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().RecieveDamage(true);
            playerHealth.IncreaseMana();
        }
    }
}

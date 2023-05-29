using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float timeBetweenShots = 0.5f;
    PlayerSpriteHandler playerSpriteHandler;

    bool canShoot = true;

    void Awake()
    {
        playerSpriteHandler = FindObjectOfType<PlayerSpriteHandler>();
    }

    public void ShootProjectile()
    {
        if(canShoot)
        {
            playerSpriteHandler.SetSpriteLooseArrow(timeBetweenShots);
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        GameObject instance = Instantiate(projectilePrefab, transform.position, transform.rotation);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * projectileSpeed;
        }

        Destroy(instance, projectileLifeTime);

        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;
    [SerializeField] float damageRecieved = 10f;
    [SerializeField] Slider healthSlider;
    [SerializeField] AudioClip playerDamageSFX;

    LevelManager levelManager;
    CameraShake cameraShake;
    PlayerSpriteHandler playerSpriteHandler;
    bool isInvincible = false;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        playerSpriteHandler = GetComponent<PlayerSpriteHandler>();
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth;
        healthSlider.value = playerHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !isInvincible)
        {
            other.gameObject.GetComponent<EnemyHealth>().RecieveDamage(false);
            StartCoroutine(RecieveDamage());
            if(playerHealth <= 0)
            {
                levelManager.LoadGameOver();
            }
        }
    }

    IEnumerator RecieveDamage()
    {
        playerHealth -= damageRecieved;
        isInvincible = true;
        healthSlider.value = playerHealth;
        playerSpriteHandler.SetSpriteDamaged();
        cameraShake.Play();
        AudioSource.PlayClipAtPoint(playerDamageSFX, transform.position);
        yield return new WaitForSeconds(0.1f);
        isInvincible = false;
    }
}

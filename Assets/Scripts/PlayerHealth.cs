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
    [SerializeField] float invincibilityTime = 2f;

    LevelManager levelManager;
    CameraShake cameraShake;
    PlayerSpriteHandler playerSpriteHandler;
    bool isInvincible = false;
    float timeElapsed;

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

    void Update()
    {
        if(isInvincible)
        {
            if (invincibilityTime < timeElapsed)
            {
                isInvincible = false;
                timeElapsed = 0f;
            }

            timeElapsed += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && !isInvincible)
        {
            other.gameObject.GetComponent<EnemyHealth>().RecieveDamage(false);
            RecieveDamage();
            if(playerHealth <= 0)
            {
                levelManager.LoadGameOver();
            }
        }
    }

    void RecieveDamage()
    {
        SetInvincibility();
        playerHealth -= damageRecieved;
        healthSlider.value = playerHealth;
        playerSpriteHandler.SetSpriteDamaged();
        cameraShake.Play();
        AudioSource.PlayClipAtPoint(playerDamageSFX, transform.position);

    }

    public void SetInvincibility()
    {
        isInvincible = true;
    }
}

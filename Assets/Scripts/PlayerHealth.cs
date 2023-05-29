using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerMaxHealth = 100;
    [SerializeField] int playerMaxMana = 100;
    [SerializeField] int damageRecieved = 20;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider manaSlider;
    [SerializeField] Image manaFillImage;
    [SerializeField] Color fullManaColor;
    [SerializeField] AudioClip playerDamageSFX;
    [SerializeField] float invincibilityTime = 2f;
    [SerializeField] ParticleSystem healVFX;
    [SerializeField] int manaIncrement;

    LevelManager levelManager;
    EnemySpawner enemySpawner;
    CameraShake cameraShake;
    PlayerSpriteHandler playerSpriteHandler;
    bool isInvincible = false;
    float timeElapsed;
    int currentMana = 0;
    int currentHealth;
    Color originalManaColor;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        playerSpriteHandler = GetComponent<PlayerSpriteHandler>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        currentHealth = playerMaxHealth;
        healthSlider.maxValue = playerMaxHealth;
        healthSlider.value = currentHealth;
        manaSlider.maxValue = playerMaxMana;
        manaSlider.value = currentMana;
        originalManaColor = manaFillImage.color;
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
            if(currentHealth <= 0)
            {
                levelManager.LoadGameOver();
            }
        }
    }

    void RecieveDamage()
    {
        SetInvincibility();
        currentHealth -= damageRecieved;
        healthSlider.value = currentHealth;
        playerSpriteHandler.SetSpriteDamaged();
        cameraShake.Play();
        AudioSource.PlayClipAtPoint(playerDamageSFX, transform.position);

    }

    public void SetInvincibility()
    {
        isInvincible = true;
    }

    public void IncreaseMana()
    {
        if(currentMana < playerMaxMana)
        {
            currentMana = currentMana + manaIncrement;
            manaSlider.value = currentMana;
            if(currentMana == playerMaxMana)
                manaFillImage.color = fullManaColor;
        }
    }

    public void HealPlayer()
    {
        if(currentMana == 100 && currentHealth < playerMaxHealth)
        {
            currentHealth = playerMaxHealth;
            currentMana = 0;
            healthSlider.value = currentHealth;
            manaSlider.value = currentMana;
            manaFillImage.color = originalManaColor;
            enemySpawner.DecreaseSpawnTime();

            healVFX.Play();
        }
    }
}

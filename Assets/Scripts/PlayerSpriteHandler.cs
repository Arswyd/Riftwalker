using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHandler : MonoBehaviour
{

    [SerializeField] Sprite playerDamageSprite;
    [SerializeField] Sprite playerLooseSprite;
    [SerializeField] Sprite playerLooseDamagedSprite;
    SpriteRenderer spriteRenderer;
    Sprite originalSprite;

    bool isDamaged = false;
    bool isArrowLoose = false;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if(isDamaged && isArrowLoose)
        {
            spriteRenderer.sprite = playerLooseDamagedSprite;
        }
        else if(isDamaged)
        {
            spriteRenderer.sprite = playerDamageSprite;
        }
        else if (isArrowLoose)
        {
            spriteRenderer.sprite = playerLooseSprite;
        }
        else
        {
            spriteRenderer.sprite = originalSprite;
        }
    }

    public void SetSpriteDamaged()
    {
        StartCoroutine(Damaged(0.1f));
    }

    public void SetSpriteLooseArrow(float delay)
    {
        StartCoroutine(LooseArrow(delay));        
    }

    IEnumerator LooseArrow(float delay)
    {
        isArrowLoose = true;
        yield return new WaitForSeconds(delay);
        isArrowLoose = false;
    }

    IEnumerator Damaged(float delay)
    {
        isDamaged = true;
        spriteRenderer.color = Color.black;

        yield return new WaitForSeconds(delay);

        spriteRenderer.color = Color.white;
        isDamaged = false;
    }
}

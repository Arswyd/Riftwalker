using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;

    [SerializeField] float dampening = 0.1f;
    [SerializeField] float slowAmount = 0.5f;
    Rigidbody2D myRigidbody;

    AIDestinationSetter aIDestinationSetter;
    AIPath aIPath;
    ScoreKeeper scoreKeeper;

    float damageRecieved;
    float originalSpeed;

    void Awake()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aIDestinationSetter.target = FindObjectOfType<PlayerMovement>().transform;
        aIPath = GetComponent<AIPath>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        myRigidbody = GetComponent<Rigidbody2D>();
        originalSpeed = aIPath.maxSpeed;
    }

    public void RecieveDamage(bool isIncreasingScore)
    {
        if(isIncreasingScore)
            scoreKeeper.IncreaseCurrentScore();
            
        Destroy(gameObject);
        GameObject instance = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(instance, 1f);
    }

    void Update()
    {
        aIPath.maxSpeed += Time.deltaTime * dampening;
    }

    void OnEnable()
    {
       aIPath.maxSpeed = Mathf.Max(originalSpeed, aIPath.maxSpeed - slowAmount); 
    }
}

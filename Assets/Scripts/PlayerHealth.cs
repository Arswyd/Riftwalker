using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;
    [SerializeField] float damageRecieved = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            playerHealth -= damageRecieved;
            if(playerHealth <= 0)
            {
                Debug.Log("I'm dead");
            }
        }
    }
}

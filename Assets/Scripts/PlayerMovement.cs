using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] bool debug;

    Vector2 moveInput;
    Vector2 mousePosition;

    Rigidbody2D myRigidbody;
    Camera cam;
    PlayerAttack playerAttack;
    PlayerHealth playerHealth;


    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<PlayerHealth>();
        cam = Camera.main;
    }

    void Update()
    {
        MovePlayer();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void MovePlayer()
    {
        myRigidbody.velocity = moveInput * movementSpeed;

        Vector2 screenPosition = cam.ScreenToWorldPoint(mousePosition);
        Vector2 lookDirection = screenPosition - myRigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        myRigidbody.rotation = angle;
    }

    void OnMousePosition(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
    }

    void OnFire()
    {
        playerAttack.ShootProjectile();
    }

    void OnDrawGizmos()
    {
        if(debug)
        {
            Vector2 screenPosition = cam.ScreenToWorldPoint(mousePosition);
            Gizmos.DrawLine(myRigidbody.position, screenPosition);
        }
    }

    void OnHeal()
    {
        playerHealth.HealPlayer();
    }
}

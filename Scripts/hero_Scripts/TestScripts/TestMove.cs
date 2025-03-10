using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isDashing = false;
    private float dashTimeLeft;
    private float lastDashTime;

    [SerializeField] private float moveSpeed = 5f; // Скорость обычного движения
    [SerializeField] private float dashSpeed = 20f; // Скорость рывка
    [SerializeField] private float dashDuration = 0.2f; // Длительность рывка
    [SerializeField] private float dashCooldown = 1f; // Время перезарядки рывка

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleDash();
        UpdateAnimations();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (!isDashing)
        {
            // Обычное движение
            rb2d.linearVelocity = new Vector2(moveInput * moveSpeed, rb2d.linearVelocity.y);
        }

        // Поворот персонажа
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time > lastDashTime + dashCooldown)
        {
            StartDash();
        }

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                EndDash();
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;

        // Задаем скорость рывка
        float dashDirection = isFacingRight ? 1 : -1;
        rb2d.linearVelocity = new Vector2(dashDirection * dashSpeed, rb2d.linearVelocity.y);
    }

    void EndDash()
    {
        isDashing = false;
        rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y); // Останавливаем рывок
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    void UpdateAnimations()
    {
        animator.SetFloat("Movement", Mathf.Abs(rb2d.linearVelocity.x));
        animator.SetBool("amIstaying?", rb2d.linearVelocity.x == 0);
    }
}
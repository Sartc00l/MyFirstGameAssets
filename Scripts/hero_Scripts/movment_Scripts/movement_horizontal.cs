using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Movement_horizontal : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animation;
    bool isRight = true;
    bool isDashing = false;


    [SerializeField] private float speedMove=13f;
    [SerializeField] private bool amIstaying = false;
    [SerializeField] private float speedDash;
    [SerializeField] private float dashCooldown = 1f; // Время перезарядки рывка



    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
    }
   
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        UpdtAnimation();
        HandleDash();
    }
    void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb2d.linearVelocity = new Vector2(moveInput * speedMove, rb2d.linearVelocity.y);

        // Поворот персонажа
        if (moveInput > 0 && !isRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        isRight = !isRight;
        transform.Rotate(0,180,0);
    }
    void IsSpeedZero()
    {
        if (rb2d.linearVelocity.x == 0)
        {
            amIstaying = true;
            animation.SetBool("amIstaying?", amIstaying);
        }
        else if (rb2d.linearVelocity.x > 0 || rb2d.linearVelocity.x < 0)
        {
            amIstaying = false;
            animation.SetBool("amIstaying?", amIstaying);
        }
    }
    void UpdtAnimation()
    {
        animation.SetFloat("Movement", Mathf.Abs(rb2d.linearVelocity.x));
        animation.SetBool("amIstaying?", rb2d.linearVelocity.x == 0);
        IsSpeedZero();
    }
    void HandleDash()
    {
        StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashCooldown);
        
    }
}

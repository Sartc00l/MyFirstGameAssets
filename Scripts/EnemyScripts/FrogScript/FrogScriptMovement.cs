using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    public Transform enemyLine;
    public Transform boundaryRight;
    public Collider2D enemyDied;
    public Rigidbody2D rigidEnemy2d;

    Animator anim;

    bool isItRight = true;
    bool isEnemySawPlayer = false;

    [SerializeField]
    int jump = 4;
    static int flipTime=5;

    void Start()
    {
        rigidEnemy2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(WaitThenFlip(flipTime));
    }

    private void Update()
    {
        Death();
    }

    private void Flip()
    {
        gameObject.transform.Rotate(0, 180, 0);
        isItRight = !isItRight;
    }

    private IEnumerator WaitThenFlip(int _flipTime)//Every 5 frames flip enemy
    {
        yield return new WaitForSeconds(_flipTime);
        if (isEnemySawPlayer == false)
        {
            Flip();
        }
        isEnemySawPlayer = false;
        StartCoroutine(WaitThenFlip(_flipTime));
    }

    private void OnDrawGizmos()
    {

        if (boundaryRight != null)
        {
            Gizmos.color = Color.red; // Цвет границ
            Gizmos.DrawLine(enemyLine.position, boundaryRight.position);
            Gizmos.DrawSphere(boundaryRight.position, 0.1f); // Правый предел
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & collision.IsTouchingLayers())//Movement enemy main logic
        {
            if (isItRight == true)
            {
                rigidEnemy2d.linearVelocity = new Vector3(10, rigidEnemy2d.linearVelocity.y, 0);
                isEnemySawPlayer = true;
                anim.SetTrigger("didIseePlayer");
                anim.SetFloat("MovementFrog", rigidEnemy2d.linearVelocity.x);
            }
            else if (isItRight == false)
            {
                rigidEnemy2d.linearVelocity = new Vector3(-10, rigidEnemy2d.linearVelocity.y, 0);
                isEnemySawPlayer = true;
                anim.SetTrigger("didIseePlayer");
                anim.SetFloat("MovementFrog", rigidEnemy2d.linearVelocity.x);
                
            }
            Debug.Log("Увидел игрока");
        }
    }


    IEnumerator WhatIsThat()
    {
        yield return new WaitForSeconds(5);
        anim.enabled = false;
    }
    void Death()
    {
        //rigidEnemy2d.velocity = new Vector3(1*1,rigidEnemy2d.velocity.y,0);
        if (GetComponent<FrogTakeDmg>().isFrogdead == true)
        {
            flipTime = 1;
            enemyDied.enabled = false;
            StartCoroutine(SecondFallDown());
            StartCoroutine(DeathTime());
        }

        IEnumerator SecondFallDown()
        {
            rigidEnemy2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            yield return new WaitForSeconds (1);
            rigidEnemy2d.AddForce(new Vector2(0, -10 * 4), ForceMode2D.Impulse);
        }
        IEnumerator DeathTime()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}

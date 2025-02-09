using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    Rigidbody2D rigidEnemy2d;
    public Transform enemyLine;
    public Transform boundaryRight;
    public Collider2D enemyDied;
    Animator anim;
    bool isItright=true;
    bool isEnemySawPlayer = false;
    
    private void Flip()
    {
        gameObject.transform.Rotate(0, 180, 0);
        isItright = !isItright;
    }
    private IEnumerator StateCoroutine()
    {
        yield return new WaitForSeconds(5);
        if (isEnemySawPlayer == false)
        {
            Flip();
        }
        isEnemySawPlayer=false;
        StartCoroutine(StateCoroutine());
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
        if (collision.CompareTag("Player")&collision.IsTouchingLayers())
        {
            if (isItright == true)
            {
                rigidEnemy2d.linearVelocity = new Vector3(10, rigidEnemy2d.linearVelocity.y, 0);
                isEnemySawPlayer = true;
                anim.SetTrigger("didIseePlayer");
                anim.SetFloat("MovementFrog",rigidEnemy2d.linearVelocity.x);
            }
            else if (isItright == false)
            {
                rigidEnemy2d.linearVelocity = new Vector3(-10, rigidEnemy2d.linearVelocity.y, 0);
                isEnemySawPlayer = true;
                anim.SetTrigger("didIseePlayer");
                anim.SetFloat("MovementFrog", rigidEnemy2d.linearVelocity.x);
            }
                Debug.Log("Увидел игрока");
        }
    }

    void Start()
    {
        rigidEnemy2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(StateCoroutine());
    }


    private void Update()
    {
        //rigidEnemy2d.velocity = new Vector3(1*1,rigidEnemy2d.velocity.y,0);
        if (GetComponent<FrogTakeDmg>().isFrogdead == true)
        {
            rigidEnemy2d.bodyType = RigidbodyType2D.Static;
            enemyDied.isTrigger = true;
            StartCoroutine();
            anim.enabled = false;
        }
    }
    IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(2);
        anim.enabled = false;
    }
}

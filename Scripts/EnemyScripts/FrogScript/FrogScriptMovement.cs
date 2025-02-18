using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    [SerializeField] Transform enemyLine;
    [SerializeField] Transform playerTransform;
    [SerializeField] Collider2D enemyDied;
    [SerializeField] Rigidbody2D rigidEnemy2d;
    [SerializeField] Animator anim;

    Vector2 direction;


    bool isItRight = true;
    bool isEnemySawPlayer = false;

    float angle;
    float distance;

    static int flipTime = 5;

    void Start()
    {
        rigidEnemy2d = GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        StartCoroutine(WaitThenFlip(flipTime));
        StartCoroutine(PositionPlayerDebug());
    }

    private void Update()
    {
        Death();
        FindPlayer();
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


    private void OnTriggerEnter2D(Collider2D collision)//Movement enemy main logic
    {
        if (collision.CompareTag("Player") & collision.IsTouchingLayers())
        {
            
                if (isItRight == true)
                {
                    rigidEnemy2d.linearVelocity = new Vector3(10,rigidEnemy2d.linearVelocity.y, 0);
                    isEnemySawPlayer = true;
                    anim.SetTrigger("didIseePlayer");
                    anim.SetFloat("MovementFrog",rigidEnemy2d.linearVelocity.x);
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

    private void FindPlayer()//Less 90 = player left, more 90 = player right
    { 
        direction = (Vector2)playerTransform.position - (Vector2)transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        distance = direction.magnitude;
        angle = Mathf.Abs(angle);
    }

    IEnumerator PositionPlayerDebug()
    {
        yield return new WaitForSeconds(2);
        FindPlayer();
        Debug.ClearDeveloperConsole();
        if (angle>90)
        {
            Debug.Log($"Игрок слева градус = {angle}");
        }
        else if(angle<90)
        {
            Debug.Log($"Игрок справа градус = {angle}");
        }
        StartCoroutine( PositionPlayerDebug());
    }
    private void Death()
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
            yield return new WaitForSeconds(1);
            rigidEnemy2d.AddForce(new Vector2(0, -10 * 4), ForceMode2D.Impulse);
        }
        IEnumerator DeathTime()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.yellow; // Цвет границ
            Gizmos.DrawLine(enemyLine.position, playerTransform.position);
            Gizmos.DrawSphere(playerTransform.position, 0.1f); // Правый предел
        }
    }

}

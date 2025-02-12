using System.Collections;
using UnityEditor;
using UnityEngine;

public class HitEnemyHand : MonoBehaviour
{
    [SerializeField] 
    float attackRange = 0.6f;
    [SerializeField]
    int giveDamage=1;

    public Transform attackPoint;
    public LayerMask enemyLayer;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            Attack();            
        }
    }
    void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<FrogTakeDmg>().TakeDamage(giveDamage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
    
    
}

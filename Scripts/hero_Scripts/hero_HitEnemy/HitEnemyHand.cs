using UnityEditor;
using UnityEngine;

public class HitEnemyHand : MonoBehaviour
{
    Animator anim;
    public float attackRange = 0.6f;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    [SerializeField]
    int giveDamage=1;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
        //Detection enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayer);
        //dmg
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

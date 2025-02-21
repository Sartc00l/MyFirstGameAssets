using System.Collections;
using UnityEngine;

public class FrogTakeDmg : MonoBehaviour
{
    public bool isFrogdead = false;

    int currentHealth;
    public int heathEnemy = 4;

    public Animator anim;
    public Transform enemyDied;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = heathEnemy;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (isFrogdead == false)
        {
            anim.SetTrigger("Hit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isFrogdead = true;
        StartCoroutine();
    }

    IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(2);
        this.enabled = false;
    }
}

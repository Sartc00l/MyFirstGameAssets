using System.Collections;
using UnityEngine;

public class FrogTakeDmg : MonoBehaviour
{
    public bool isFrogdead = false;

    int currentHealth;
    public int heathEnemy = 4;

    public Animator anim;
    public Transform enemyDied;
    FrogScript frg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        frg = GetComponent<FrogScript>();
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
        Debug.Log("Жабка умерла");
        anim.SetBool("Dead?", true);
        isFrogdead = true;
       // frg.rigidEnemy2d.bodyType = RigidbodyType2D.Static;
        //frg.enabled = false;
        StartCoroutine();
    }

    IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(2);
        this.enabled = false;
    }
}

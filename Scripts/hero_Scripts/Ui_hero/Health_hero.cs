using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health_hero : MonoBehaviour
{
    public heartUi heart = new heartUi();
    [SerializeField]
    int health = 4;
    bool didIhitted = false;
    private void Start()
    {
        StartCoroutine();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemyy"))
        {
            didIhitted = true;
            health--;
            if (health > 0)
            {
                heart.SetSprite(health);
            }
            else if (health<0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private IEnumerator StartCoroutine()
    {
        
        yield return new WaitForSeconds(4);
        didIhitted = false;
        StartCoroutine();
    }
}

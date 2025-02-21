using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class frog_death : MonoBehaviour
{
    public GameObject frogAlive;
    public Transform posFrog;

    Rigidbody2D rb2t;

    void Start()
    {
        rb2t = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(SetNonStaticRb());
    }
    IEnumerator SetNonStaticRb()
    {
        yield return new WaitForSeconds(1);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

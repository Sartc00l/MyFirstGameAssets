using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JumpScript : MonoBehaviour
{
    Rigidbody2D rb2t;
    Animator anim;
    bool amIstaying = false;
    [SerializeField]
    int jumpStong=350;

 
    // Start is called before the first frame update
    void Start()
    {
        rb2t = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Terrarian"))
        {
            amIstaying = true;
            anim.SetBool("IsStable", amIstaying);  
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrarian"))
        {
            amIstaying = false;
            anim.SetBool("IsStable", amIstaying);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) & amIstaying)
        {
            rb2t.AddForce(new Vector2(0, jumpStong),ForceMode2D.Impulse);
            anim.SetTrigger("amIJumped?");
        }
        anim.SetFloat("ySpeed", rb2t.linearVelocity.y);

    }
}

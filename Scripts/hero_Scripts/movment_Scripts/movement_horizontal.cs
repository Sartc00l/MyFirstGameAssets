using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class movement_horizontal : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animation;
    bool isRight = true;
    [SerializeField]
    bool amIstaying = false;
    float mov;

    int[] speedArray = Enumerable.Range(1, 14).ToArray();

    float move;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animation = gameObject.GetComponent<Animator>();
    }
    private void IsSpeedZero()
    {
        if (rb2d.linearVelocity.x == 0)
        {
            amIstaying = true;
            animation.SetBool("amIstaying?", amIstaying);
        }
        else if (rb2d.linearVelocity.x > 0 || rb2d.linearVelocity.x < 0)
        {
            amIstaying = false;
            animation.SetBool("amIstaying?", amIstaying);
        }
    }
    // Update is called once per frame
    void Update()
    {


        mov = Input.GetAxis("Horizontal");
        if (mov > 0)
        {
            for (int time = 0; time <= 12; time++)
            {
                mov = Input.GetAxis("Horizontal");

                move = speedArray[time] * mov;

                speed = move;

                rb2d.linearVelocity = new Vector3(mov * speed, rb2d.linearVelocity.y, 0);
            }
        }
        if (mov < 0)
        {
            for (int time = 0; time <= 12; time++)
            {
                mov = Input.GetAxis("Horizontal");

                move = speedArray[time] * mov;

                speed = move;

                rb2d.linearVelocity = new Vector3(mov * speed * -1, rb2d.linearVelocity.y, 0);
            }
        }

        animation.SetFloat("Movement", Mathf.Abs(rb2d.linearVelocity.x));
        if (rb2d.linearVelocity.x > 0 & !isRight)
        {
            gameObject.transform.Rotate(0, 180, 0);
            isRight = true;
        }
        else if (rb2d.linearVelocity.x < 0 & isRight)
        {
            gameObject.transform.Rotate(0, 180, 0);
            isRight = false;
        }
        IsSpeedZero();
    }

}

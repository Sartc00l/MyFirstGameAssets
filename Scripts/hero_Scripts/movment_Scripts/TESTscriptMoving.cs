using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTscriptMoving : MonoBehaviour
{

    // Start is called before the first frame update
    Rigidbody2D rb2d;
    [SerializeField]
    float mov;
    [SerializeField]
    float speed=0f;
    [SerializeField]
    float testSpeed=0f;
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mov = Input.GetAxis("Horizontal");
        testSpeed = mov * speed;
        rb2d.linearVelocity = new Vector3(mov * speed , rb2d.linearVelocity.y, 0);


    }
}

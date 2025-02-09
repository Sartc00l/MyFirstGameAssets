using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool isDestroyed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.name == "Character")
        {
            Destroy(this.gameObject);
            isDestroyed = true;
        }
    }
   
}
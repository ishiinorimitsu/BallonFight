using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed;
    public void Shoot(GameObject player)
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(speed*player.transform.localScale.x,rigidbody2D.velocity.y);
        Vector2 tempF = transform.localScale;
        if(player.transform.localScale.x > 0)
        {
            tempF.x = 0.13f;
        }else if(player.transform.localScale.x < 0)
        {
            tempF.x = -0.13f;
        }
        transform.localScale = tempF;
        Destroy(gameObject, 5);
        Debug.Log("¬Œ÷");
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}

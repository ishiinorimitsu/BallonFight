using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("�ړ����x")]
    public float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-moveSpeed,0,0);
        if(transform.position.x < -14.0f)
        {
            Destroy(gameObject);
        }
    }
}

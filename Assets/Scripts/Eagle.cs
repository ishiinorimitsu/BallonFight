using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public float eagleHp;

    [SerializeField]
    private EnemyDestroy enemyDestroy;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            eagleHp -= 20;
            if (eagleHp <= 0)
            {
                enemyDestroy.EnemyObjectDestroy(gameObject);
            }
        }
    }
}

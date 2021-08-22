using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject destroyEffect;

    [SerializeField]
    private GameObject fuelEnergy;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Fire")
        {
            GameObject effect = Instantiate(destroyEffect, transform.position,destroyEffect.transform.rotation);
            Destroy(effect, 1.0f);
            GameObject energy = Instantiate(fuelEnergy, transform.position, fuelEnergy.transform.rotation);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject destroyEffect;

    [SerializeField]
    private GameObject fuelEnergy;

    [SerializeField,Range(0,100)]
    private int generateValue;

    [SerializeField]
    private UIManager uimanagar;

    [SerializeField]
    private int getDestroyDamage;  //敵が破壊された時の得点

    //public Mouse mouse;

    public void EnemyObjectDestroy(GameObject gameObject)
    {
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.coinPoint += getDestroyDamage;
        uimanagar.UpdateDisplayScore(playerController.coinPoint);
        GameObject effect = Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
        Destroy(effect, 1.0f);
        RandomEnergyGenerate();
        Destroy(gameObject);
    }


    //public void OnCollisionEnter2D(Collision2D col)
    //{
    //    if(col.gameObject.tag == "Fire")
    //    {
    //        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //        playerController.coinPoint += getDestroyDamage;
    //        uimanagar.UpdateDisplayScore(playerController.coinPoint);
    //        GameObject effect = Instantiate(destroyEffect, transform.position,destroyEffect.transform.rotation);
    //        Destroy(effect, 1.0f);
    //        RandomEnergyGenerate();
    //        Destroy(gameObject);
    //    }
    //}

    public void SetUpEnemy(UIManager uIManager)
    {
        this.uimanagar = uIManager;
    }

    private void RandomEnergyGenerate()
    {
        //0から100の数字から抽選する
        int randomValue = Random.Range(0, 100);

        //抽選した数字が指定した数字よりも小さいとき
        if (randomValue < generateValue)
        {
            //アイテムを生成する
            GameObject energy = Instantiate(fuelEnergy, transform.position, fuelEnergy.transform.rotation);
        }
    }
}

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
    private int getDestroyDamage;  //�G���j�󂳂ꂽ���̓��_

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
        //0����100�̐������璊�I����
        int randomValue = Random.Range(0, 100);

        //���I�����������w�肵�����������������Ƃ�
        if (randomValue < generateValue)
        {
            //�A�C�e���𐶐�����
            GameObject energy = Instantiate(fuelEnergy, transform.position, fuelEnergy.transform.rotation);
        }
    }
}

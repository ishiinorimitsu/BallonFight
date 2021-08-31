using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float mouseHp;

    //[SerializeField]
    //private GameObject destroyEffect;

    //[SerializeField]
    //private GameObject fuelEnergy;

    //[SerializeField, Range(0, 100)]
    //private int generateValue;

    //[SerializeField]
    //private UIManager uimanagar;

    //[SerializeField]
    //private int getDestroyDamage;

    [SerializeField]
    private EnemyDestroy enemyDestroy;

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Fire")
        {
            mouseHp -= 20;
            if (mouseHp <= 0)
            {
                enemyDestroy.EnemyObjectDestroy(gameObject);
            }
        }
    }

    //public void EnemyObjectDestroy(GameObject gameObject)
    //{
    //    PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //    playerController.coinPoint += getDestroyDamage;
    //    uimanagar.UpdateDisplayScore(playerController.coinPoint);
    //    GameObject effect = Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
    //    Destroy(effect, 1.0f);
    //    RandomEnergyGenerate();
    //    Destroy(gameObject);
    //}

    //public void SetUpEnemy(UIManager uIManager)
    //{
    //    this.uimanagar = uIManager;
    //}

    //private void RandomEnergyGenerate()
    //{
    //    //0����100�̐������璊�I����
    //    int randomValue = Random.Range(0, 100);

    //    //���I�����������w�肵�����������������Ƃ�
    //    if (randomValue < generateValue)
    //    {
    //        //�A�C�e���𐶐�����
    //        GameObject energy = Instantiate(fuelEnergy, transform.position, fuelEnergy.transform.rotation);
    //    }
    //}
}

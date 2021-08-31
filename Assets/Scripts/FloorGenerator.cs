using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    //ヒエラルギーのゲームオブジェクトはアサインできるが、Prefabのものはprivateで宣言して箱を作っておいて、メソッドを通じて必要なヒエラルギーの情報をもらう。
    [SerializeField]
    private GameObject aerialFloorPrefab;

    [SerializeField]
    private Transform generateTran;

    [Header("生成までの待機時間")]
    public float waitTime;

    private float timer;

    private GameDirector gameDirector;

    private bool isActivate;

    // Update is called once per frame
    void Update()
    {
        if(isActivate == false)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            timer = 0;
            GenerateFloor();
        }
    }

    public GameObject GenerateFloor()
    {
        GameObject obj = Instantiate(aerialFloorPrefab,generateTran);
        float randomPosY = Random.Range(-4.0f, 4.0f);
        obj.transform.position = new Vector2(obj.transform.position.x,obj.transform.position.y + randomPosY);
        gameDirector.GenerateCount++;
        return obj;
    }

    public void SetUpGenerator(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;
    }

    public void SwitchActivation(bool isSwitch)
    {
        isActivate = isSwitch;
    }
}

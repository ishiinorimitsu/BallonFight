using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objPrefab;

    [SerializeField]
    private Transform generateTran;

    [Header("生成までの待機時間")]
    public Vector2 waitTimeRange;

    private float waitTime;
    private float timer;
    private bool isActivate;  //trueなら生成
  
    private GameDirector gameDirector;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private int[] randomValues;

    // Start is called before the first frame update
    void Start()
    {
        SetGenerateTime();
        gameDirector = this.GetComponent<GameDirector>();
    }

    private void SetGenerateTime()
    {
        // 生成までの待機時間を、最小値と最大値の間からランダムで設定
        waitTime = Random.Range(waitTimeRange.x,waitTimeRange.y);
    }
    void Update()
    {
        if(isActivate == false)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer >= waitTime)
        {
            timer = 0;
            RandomGenerateObject();
        }
    }

    public void RandomGenerateObject()
    {
        //int randomIndex = Random.Range(0, objPrefab.Length);
        int totalValue = 0;
        for(int i = 0; i < randomValues.Length; i++)
        {
            totalValue += randomValues[i];
        }
        int random = Random.Range(0,totalValue);
        int x = 0;
        int randomIndex = 0;
        for(int i = 0;i < randomValues.Length; i++)
        {
            x += randomValues[i];
            if (random < x)
            {
                randomIndex = i;
                break;
            }
        }
        GameObject obj = Instantiate(objPrefab[randomIndex], generateTran);
        if(randomIndex == 2)
        {
            //床を生成する
            gameDirector.CreateFloorToMouse(obj);
        }
        else
        {
            float randomPosY = Random.Range(-4.0f, 4.0f);
            obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);
        }
        if(obj.TryGetComponent(out EnemyDestroy enemyDestroy) == true)
        {
            enemyDestroy.SetUpEnemy(uiManager);
        }
        SetGenerateTime();
    }

    public void SwitchActivation(bool isSwitch)
    {
        isActivate = isSwitch;
    }
}

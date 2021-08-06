using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private GoalChecker goalHousePrefab;            // ゴール地点のプレファブをアサイン

    [SerializeField]
    private PlayerController playerController;      // ヒエラルキーにある Yuko_Player ゲームオブジェクトをアサイン

    [SerializeField]
    private FloorGenerator[] floorGenerators;       // floorGenerator スクリプトのアタッチされているゲームオブジェクトをアサイン

    private bool isSetUp;                           // ゲームの準備判定用。true になるとゲーム開始

    private bool isGameUp;                          // ゲーム終了判定用。true になるとゲーム終了

    private int generateCount;                      //床の製造個数

    public int GenerateCount
    {
        set
        {
            generateCount = value;
            Debug.Log("生成数 / クリア目標数 : " + generateCount + " / " + clearCount);

            if(generateCount >= clearCount)
            {
                GenerateGoal();
                GameUp();
            }
        }
        get
        {
            return generateCount;
        }
    }

    public int clearCount;

    void Start()
    {
        isGameUp = false;
        isSetUp = false;

        SetUpFloarGenerators();

        Debug.Log("生成停止");
    }

    private void SetUpFloarGenerators()
    {
        for(int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SetUpGenerator(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isFirstGenerateBallon && isSetUp == false)
        {

            // 準備完了
            isSetUp = true;

            // TODO 各ジェネレータを動かし始める
            Debug.Log("生成スタート");
        }
    }
    private void GenerateGoal()
    {
        // ゴール地点を生成
        GoalChecker goalHouse = Instantiate(goalHousePrefab);

        // TODO ゴール地点の初期設定
        Debug.Log("ゴール地点 生成");
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void GameUp()
    {

        // ゲーム終了
        isGameUp = true;

        // TODO 各ジェネレータを停止
        Debug.Log("生成停止");
    }
}

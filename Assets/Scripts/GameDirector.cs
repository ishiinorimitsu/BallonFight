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

    [SerializeField]
    private RandomObjectGenerator[] randomObjectGenerators;

    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private StartChecker StartChecker;

    private bool isSetUp;                           // ゲームの準備判定用。true になるとゲーム開始

    private bool isGameUp;                          // ゲーム終了判定用。true になるとゲーム終了

    private int generateCount;                      //床の製造個数

    private string start = "Start";

    public int GenerateCount
    {
        set    //代入するときに呼び出される
        {
            generateCount = value;　//代入する値がvalueに入る
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

    public void CreateFloorToMouse(GameObject obj)
    {
        GameObject floor = floorGenerators[0].GenerateFloor();
        obj.transform.SetParent(floor.transform);
        obj.transform.localPosition = new Vector3(2.44f,0.86f,0);
    }

    void Start()
    {
        StartCoroutine(audioManager.PlayBGM(0));

        isGameUp = false;
        isSetUp = false;

        SetUpFloarGenerators();

        StopGenerators();
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
        if (Input.GetButtonDown(start))
        {
            Debug.Log("Start");
            if (isSetUp == false)
            {
                StartCoroutine(audioManager.PlayBGM(1));

                // 準備完了
                isSetUp = true;

                // TODO 各ジェネレータを動かし始める
                Debug.Log("生成スタート");

                ActivateGenerators();

                StartChecker.SetInitialSpeed();
            }
        }
    }
    private void GenerateGoal()
    {
        // ゴール地点を生成
        GoalChecker goalHouse = Instantiate(goalHousePrefab);

        goalHouse.SetUpGoalHouse(this);

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

        StopGenerators();
    }

    private void StopGenerators()
    {
        for(int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(false);
        }
        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(false);
        }
    }

    private void ActivateGenerators()
    {
        for (int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(true);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(true);
        }
    }

    public void GoalClear()
    {
        // クリアの曲再生
        StartCoroutine(audioManager.PlayBGM(2));

    }
}

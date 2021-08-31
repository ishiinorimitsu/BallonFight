using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private string horizontal = "Horizontal";

    private string jump = "Jump";

    private string fire1 = "Fire1";

    private Rigidbody2D rb;

    public float moveSpeed;

    public float jumpPower;

    public bool isGrounded;

    public GameObject[] ballons;

    public int MaxBallonCount;

    public Transform[] ballonTrans;

    public Transform[] fireTrans;

    public GameObject ballonPrefab;

    public float generateTime;

    public bool isGenerating;

    public bool isFirstGenerateBallon;

    public float knockBackPower;

    public int coinPoint;   //コインを一つ取った時のポイント

    public int energyPoint;

    public UIManager uiManager;

    public GameObject firePrefab;

    public ParticleSystem rocketEffect;

    public int maxEnergy;      //所持エネルギー（マックスエネルギー）

    public int jumpEnergy; 　　//一回ジャンプするごとに消費するエネルギー、1を基準として％で計算するよりも、持ち点のように100あって、10ずつ減るみたいなほうが作りやすい

    public int attackEnergy;　　//一回攻撃するごとに使うエネルギー

    [SerializeField, Header("Linecast用 地面判定レイヤー")]
    private LayerMask groundLayer;

    private float scale;

    private Animator anim;

    private float limitPosX = 9.5f;

    private float limitPosY = 4.45f;

    [SerializeField]
    private StartChecker startChecker;

    private bool isGameOver;

    [SerializeField]
    private AudioClip knockBackSE;

    [SerializeField]
    private GameObject knockBackEffectPrefab;

    [SerializeField]
    private AudioClip coinSE;

    [SerializeField]
    private GameObject coinEffectPrefab;

    [SerializeField]
    private Joystick joystick;                        // FloatingJoystick ゲームオブジェクトにアタッチされている FloatingJoystick スクリプトのアサイン用

    [SerializeField]
    private Button btnJump;                           // btnJump ゲームオブジェクトにアタッチされている Button コンポーネントのアサイン用

    [SerializeField]
    private Button btnDetach;                         // btnDetachOrGenerate ゲームオブジェクトにアタッチされている Button コンポーネントのアサイン用

    private int ballonCount;

    [SerializeField]
    private int currentEnergy;       //現在の残りエネルギー

    private float energyTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
        anim = GetComponent<Animator>();
        ballons = new GameObject[MaxBallonCount];
        //btnJump.onClick.AddListener(OnClickJump);
        //btnDetach.onClick.AddListener(OnClickDetachOrGenerate);
        currentEnergy = maxEnergy;
        uiManager.SetSliderValue(maxEnergy);
    }

    private void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);

        if (isGrounded == true)
        {
            energyTimer ++; 
            if(energyTimer >= 10)
            {
                currentEnergy += 1;
                energyTimer = 0;
                uiManager.UpdateDisplayEnergy(currentEnergy);
            }
        }

        if (currentEnergy >= 100)
        {
            currentEnergy = 100;
            uiManager.UpdateDisplayEnergy(currentEnergy);
        }

        //if (ballons[0] != null)
        //{
        //    if (Input.GetButtonDown(jump))
        //    {
        //        Jump();
        //    }

        //    if (isGrounded == false && rb.velocity.y < 0.15f)
        //    {
        //        anim.SetTrigger("Fall");
        //    }
        //}
        //else
        //{
        //    //Debug.Log("バルーンがないので、ジャンプできません");
        //}

        if (Input.GetButtonDown(jump))
        {
            Jump();
        }

        if (rb.velocity.y > 5.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x,5);
        }

        //if(isGrounded == true && isGenerating == false)
        //{
        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        StartCoroutine(GenerateBallon());
        //    }
        //}

        if (Input.GetButtonDown(fire1))
        {
            if (currentEnergy >= attackEnergy)
            {
                GameObject firepre = Instantiate(firePrefab, fireTrans[0].position, firePrefab.transform.rotation);
                firepre.GetComponent<Fire>().Shoot(gameObject);
                currentEnergy -= attackEnergy;
                uiManager.UpdateDisplayEnergy(currentEnergy);
            }
        }


        //攻撃用のボタンを押したらFirePrefabをInstantiate()
        //shoot()メソッドを発動
    }

    public void Jump()
    {
        if (currentEnergy >= jumpEnergy)
        {
            currentEnergy -= jumpEnergy;
            rb.AddForce(transform.up * jumpPower);
            anim.SetTrigger("Jump");
            rocketEffect.Play();
            uiManager.UpdateDisplayEnergy(currentEnergy);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGameOver == true)
        {
            return;
        }
        Move();
    }

    private void Move()
    {
#if UNITY_EDITOR
        // 水平(横)方向への入力受付
        float x = Input.GetAxis(horizontal);
        //x = joystick.Horizontal; 
#else
        float x = joystick.Horizontal;
#endif
        if(x != 0)
        {
            rb.velocity = new Vector2(x*moveSpeed,rb.velocity.y);
            Vector3 temp = transform.localScale;
            temp.x = x;
            if(x > 0)
            {
                temp.x = scale;
            }
            else
            {
                temp.x = -scale;
            }
            transform.localScale = temp;
            anim.SetBool("Idle", false);
            anim.SetFloat("Run", 0.5f);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetFloat("Run", 0.0f);
            anim.SetBool("Idle", true);
        }

        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);

        transform.position = new Vector2(posX, posY);
    }

    //private IEnumerator GenerateBallon()
    //{
    //    if (ballons[1] != null)
    //    {
    //        yield break;
    //    }

    //    isGenerating = true;

    //    if(isFirstGenerateBallon == false)
    //    {
    //        isFirstGenerateBallon = true;
    //        Debug.Log("初回のバルーン生成");
    //        startChecker.SetInitialSpeed();
    //    }

    //    if(ballons[0] == null) {
    //        ballons[0] = Instantiate(ballonPrefab, ballonTrans[0]);
    //        ballons[0].GetComponent<Ballon>().SetUpBallon(this);
    //    }
    //    else
    //    {
    //        ballons[1] = Instantiate(ballonPrefab, ballonTrans[1]);
    //        ballons[1].GetComponent<Ballon>().SetUpBallon(this);
    //    }

    //    ballonCount++;

    //    yield return new WaitForSeconds(generateTime);

    //    isGenerating = false;
    //}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            //Vector3 direction = (transform.position - col.transform.position).normalized;
            //transform.position += direction * knockBackPower;
            //AudioSource.PlayClipAtPoint(knockBackSE, transform.position);
            //GameObject knockBackEffect = Instantiate(knockBackEffectPrefab, col.transform.position, Quaternion.identity);
            //Destroy(knockBackEffect, 0.5f);
            currentEnergy -= 10;
            uiManager.UpdateDisplayEnergy(currentEnergy);
        }
    }

    //public void DestroyBallon()
    //{
    //    if(ballons[1] != null)
    //    {
    //        Destroy(ballons[1]);
    //    }else if(ballons[0] != null)
    //    {
    //        Destroy(ballons[0]);
    //    }

    //    ballonCount--;
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Coin")
        {
            coinPoint += col.gameObject.GetComponent<Coin>().getCoinPoint;
            uiManager.UpdateDisplayScore(coinPoint);
            Destroy(col.gameObject);
            AudioSource.PlayClipAtPoint(coinSE, transform.position);
            GameObject coinEffect = Instantiate(coinEffectPrefab, col.transform.position, Quaternion.identity);
        }
        else if(col.gameObject.tag == "Energy")
        {
            coinPoint += col.gameObject.GetComponent<Energy>().getEnergyPoint;
            uiManager.UpdateDisplayScore(coinPoint);
            currentEnergy += 100;
            Destroy(col.gameObject);
            Debug.Log("Hit");
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("isGameOver");
        uiManager.DisplayGameOverInfo();
    }

    //private void OnClickJump()
    //{
    //    // バルーンが１つ以上あるなら
    //    if (ballonCount > 0)
    //    {
    //        Jump();
    //    }
    //}

    /// <summary>
    /// バルーン生成ボタンを押した際の処理
    /// </summary>
    //private void OnClickDetachOrGenerate()
    //{

    //    // 地面に接地していて、バルーンが２個以下の場合
    //    if (isGrounded == true && ballonCount < MaxBallonCount && isGenerating == false)
    //    {

    //        // バルーンの生成中でなければ、バルーンを１つ作成する
    //        StartCoroutine(GenerateBallon());
    //    }
    //}
}

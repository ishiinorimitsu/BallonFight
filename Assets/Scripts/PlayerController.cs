using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private string horizontal = "Horizontal";
    private string jump = "Jump";
    private Rigidbody2D rb;
    public float moveSpeed;
    public float jumpPower;
    public bool isGrounded;
    public GameObject[] ballons;
    public int MaxBallonCount;
    public Transform[] ballonTrans;
    public GameObject ballonPrefab;
    public float generateTime;
    public bool isGenerating;
    public bool isFirstGenerateBallon;
    public float knockBackPower;
    public int coinPoint;
    public UIManager uiManager;
    [SerializeField, Header("Linecast�p �n�ʔ��背�C���[")]
    private LayerMask groundLayer;
    private float scale;
    private Animator anim;
    private float limitPosX = 9.5f;
    private float limitPosY = 4.45f;
    [SerializeField]
    private StartChecker startChecker;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale.x;
        anim = GetComponent<Animator>();
        ballons = new GameObject[MaxBallonCount];
    }

    private void Update()
    {
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);


        if (ballons[0] != null)
        {
            if (Input.GetButtonDown(jump))
            {
                Jump();
            }

            if (isGrounded == false && rb.velocity.y < 0.15f)
            {
                anim.SetTrigger("Fall");
            }
        }
        else
        {
            Debug.Log("�o���[�����Ȃ��̂ŁA�W�����v�ł��܂���");
        }

        if (rb.velocity.y > 5.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x,5);
        }

        if(isGrounded == true && isGenerating == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(GenerateBallon());
            }
        }
    }

    public void Jump()
    {
        rb.AddForce(transform.up * jumpPower);
        anim.SetTrigger("Jump");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis(horizontal);
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

    private IEnumerator GenerateBallon()
    {
        if (ballons[1] != null)
        {
            yield break;
        }

        isGenerating = true;

        if(isFirstGenerateBallon == false)
        {
            isFirstGenerateBallon = true;
            Debug.Log("����̃o���[������");
            startChecker.SetInitialSpeed();
        }

        if(ballons[0] == null) {
            ballons[0] = Instantiate(ballonPrefab, ballonTrans[0]);
            ballons[0].GetComponent<Ballon>().SetUpBallon(this);
        }
        else
        {
            ballons[1] = Instantiate(ballonPrefab, ballonTrans[1]);
            ballons[1].GetComponent<Ballon>().SetUpBallon(this);
        }

        yield return new WaitForSeconds(generateTime);

        isGenerating = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Vector3 direction = (transform.position - col.transform.position).normalized;
            transform.position += direction * knockBackPower;
        }
    }

    public void DestroyBallon()
    {
        if(ballons[1] != null)
        {
            Destroy(ballons[1]);
        }else if(ballons[0] != null)
        {
            Destroy(ballons[0]);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Coin")
        {
            coinPoint += col.gameObject.GetComponent<Coin>().point;
            uiManager.UpdateDisplayScore(coinPoint);
            Destroy(col.gameObject);
        }
    }
}

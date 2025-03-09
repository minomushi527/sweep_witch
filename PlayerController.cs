using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid2D;
    Animator animator;
    public float movespeed = 3.0f;
    float vx = 0;
    float vy = 0;
    float jumpForce = 680.0f;
    public GameObject attackSensorController;
    private int key = -1;
    private Pl_se pl_se;
    public bool isKnockback = false;//ノックバック中はtrue
    public float fly_cooldownTime = 1f; // 飛行クールタイムの長さ（秒）
    private float fly_nextAllowedTime = 0f; // 飛行の次に入力を受け付ける時刻
    private bool fly_ok = true;  //飛行魔法が使えるかどうか
    private IsGround isground;
    //float walkForce = 30.0f;
    // float maxWalkSpeed = 2.0f;
    public GameObject Gamedirector_ui;
    public int fly_use_mp = 2; //飛行するのに使用するmp
    private bool atacking = false; //atackしてる最中は再度atack出来ない


    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isground = GetComponentInChildren<IsGround>();
        attackSensorController.SetActive(false);
        pl_se = GetComponent<Pl_se>();
    }

    // Update is called once per frame
    void Update()
    {
        GameDirector gameDirector = Gamedirector_ui.GetComponent<GameDirector>();
        isKnockback = GetComponentInChildren<PlayerDamageController>().isKnockbackchild;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        //???t???[?????l????????
        vx = 0;
        vy = 0;

        // 地面についてたら解除
        if (isground.isGrounded && (this.animator.GetInteger("JumpInt_L") > 0 || this.animator.GetInteger("JumpInt_R") > 0))
        {
            this.animator.SetInteger("JumpInt_L", 0);
            this.animator.SetInteger("JumpInt_R", 0);
        }
        else if (!isground.isGrounded && (this.animator.GetInteger("JumpInt_L") == 0 || this.animator.GetInteger("JumpInt_R") == 0))
        {
            switch (key)
            {
                case -1:
                    if (!isKnockback)
                    {
                        if (this.rigid2D.velocity.y > 0)
                        {
                            this.animator.SetInteger("JumpInt_L", 1);
                        }
                        else if (this.rigid2D.velocity.y < 0)
                        {
                            this.animator.SetInteger("JumpInt_L", 2);
                        }
                    }
                    break;
                case 1:
                    if (!isKnockback)
                    {
                        if (this.rigid2D.velocity.y > 0)
                        {
                            this.animator.SetInteger("JumpInt_R", 1);
                        }
                        else if (this.rigid2D.velocity.y < 0)
                        {
                            this.animator.SetInteger("JumpInt_R", 2);
                        }
                    }
                    break;
            }
        }

        // ?????E????????????A?E????????
        if (Input.GetKey(KeyCode.RightArrow) && !isKnockback)
        {
            vx = key * movespeed;
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                this.animator.SetBool("runBool_R", true);
            }
        }
        else
        {
            this.animator.SetBool("runBool_R", false);
        }
        // ??????????????????A??????????
        if (Input.GetKey(KeyCode.LeftArrow) && !isKnockback)
        {
            vx = key * movespeed;
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
            {
                this.animator.SetBool("runBool_L", true);
            }
        }
        else
        {
            this.animator.SetBool("runBool_L", false);
        }



        // ?v???C??????x(translate)
        float speedx = Mathf.Abs(vx);

        // if (speedx < this.maxWalkSpeed)
        // {
        this.transform.Translate(vx * movespeed * Time.deltaTime, vy * movespeed * Time.deltaTime, 0);
        // }

        //ジャンプ処理
        // ジャンプアニメーション [JumpInt_R] or [JumpInt_L]
        // 0:walk,stand  1:jump up  2:fall 

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow) && this.rigid2D.velocity.y == 0 && !isKnockback && (this.animator.GetInteger("JumpInt_R") == 0 || this.animator.GetInteger("JumpInt_L") == 0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
        {
            if (atacking)
            {
                AttackEnd();
            }
            this.rigid2D.AddForce(transform.up * this.jumpForce * 1.5f);
            switch (key)
            {
                case -1:
                    if (!isKnockback)
                    {
                        pl_se.jumping();
                        this.animator.SetInteger("JumpInt_L", 1);
                        Debug.Log("JumpInt_L" + this.animator.GetInteger("JumpInt_L"));
                    }
                    break;
                case 1:
                    if (!isKnockback)
                    {
                        pl_se.jumping();
                        this.animator.SetInteger("JumpInt_R", 1);
                    }
                    break;
            }
        }

        // 落下
        if (this.rigid2D.velocity.y < 0 && !isKnockback && (this.animator.GetInteger("JumpInt_R") > 0 || this.animator.GetInteger("JumpInt_L") > 0))
        {
            switch (key)
            {
                case -1:
                    if (!isKnockback)
                    {
                        this.animator.SetInteger("JumpInt_L", 2);
                        Debug.Log("JumpInt_L" + this.animator.GetInteger("JumpInt_L"));
                    }
                    break;
                case 1:
                    if (!isKnockback)
                    {
                        this.animator.SetInteger("JumpInt_R", 2);
                    }
                    break;
            }
        }

        // 着地
        if (this.rigid2D.velocity.y == 0 && (this.animator.GetInteger("JumpInt_R") == 2 || this.animator.GetInteger("JumpInt_L") == 2))
        {
            switch (key)
            {
                case -1:
                    if (!isKnockback)
                    {
                        this.animator.SetInteger("JumpInt_L", 0);
                        Debug.Log("JumpInt_L" + this.animator.GetInteger("JumpInt_L"));
                    }
                    break;
                case 1:
                    if (!isKnockback)
                    {
                        this.animator.SetInteger("JumpInt_R", 0);
                    }
                    break;
            }
        }

        // 飛行
        //fly_cooldownTime // 飛行クールタイムの長さ（秒）
        //fly_nextAllowedTime // 飛行の次に入力を受け付ける時刻
        //fly_ok が true だったら飛行魔法が使える
        if ((this.animator.GetInteger("JumpInt_R") > 0 || this.animator.GetInteger("JumpInt_L") > 0) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !isground.isGrounded)
        {
            Debug.Log("JumpInt_L" + this.animator.GetInteger("JumpInt_L"));
            // 落下低速
            if (this.rigid2D.velocity.y < 0)
            {
                this.rigid2D.velocity = new Vector2(this.rigid2D.velocity.x, -0.1f);
            }
            if (Input.GetKey(KeyCode.UpArrow) && fly_ok)
            {
                if (gameDirector.Flyok())
                {
                    fly_nextAllowedTime = 0f;
                    fly_ok = false;
                    this.rigid2D.AddForce(transform.up * this.jumpForce);
                    pl_se.fly();
                    gameDirector.DecreaseMp(fly_use_mp);
                }
                else
                {
                    fly_nextAllowedTime = 0f;
                    fly_ok = false;
                    pl_se.cantfly();
                }

            }
            switch (key)
            {
                case -1:
                    if (!isKnockback)
                    {
                        this.animator.SetInteger("JumpInt_L", 3);
                        Debug.Log("JumpInt_L" + this.animator.GetInteger("JumpInt_L"));
                    }
                    break;
                case 1:
                    if (!isKnockback)
                    {
                        this.animator.SetInteger("JumpInt_R", 3);
                    }
                    break;
            }
        }

        if (!fly_ok)
        {
            fly_nextAllowedTime += Time.deltaTime;
            if (fly_nextAllowedTime > fly_cooldownTime)
            {
                fly_ok = true;
            }
        }
        // 飛行解除
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift) && (this.animator.GetInteger("JumpInt_R") == 3 || this.animator.GetInteger("JumpInt_L") == 3))
        {
            switch (key)
            {
                case -1:
                    if (!isKnockback)
                    {
                        this.animator.SetInteger("JumpInt_L", 2);
                        Debug.Log("JumpInt_L" + this.animator.GetInteger("JumpInt_L"));
                    }
                    break;
                case 1:
                    if (!isKnockback)
                    {
                        this.animator.SetInteger("JumpInt_R", 2);
                    }
                    break;
            }
        }

        // ?v???C??????x(AddForce)
        //float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // ?X?s?[?h????
        //if (speedx < this.maxWalkSpeed)
        // {
        //this.rigid2D.AddForce(transform.right * key * this.walkForce);
        // }

        //?W?????v???????


        //?U?????????
        if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftArrow) && !isKnockback && !atacking)
        {
            this.animator.SetTrigger("AttackTrigger_L");
            AttackStart_L();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.RightArrow) && !isKnockback && atacking == false)
        {
            this.animator.SetTrigger("AttackTrigger_R");
            AttackStart_R();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && !isKnockback && atacking == false)
        {
            AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
            if (Input.GetKeyDown(KeyCode.Z) && key == 1 && atacking == false)
            {
                this.animator.SetTrigger("AttackTrigger_R");
                AttackStart_R();
            }
            if (Input.GetKeyDown(KeyCode.Z) && key == -1 && atacking == false)
            {
                this.animator.SetTrigger("AttackTrigger_L");
                AttackStart_L();
            }
        }

    }

    void AttackStart_R()
    {
        // AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        // if (stateInfo.IsName("pl_atk_R"))
        // {

        atacking = true;
        attackSensorController.SetActive(true);
        attackSensorController.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        // }
        // else
        // {
        //     this.AttackEnd();
        // }
    }
    void AttackStart_L()
    {
        atacking = true;
        attackSensorController.SetActive(true);
        attackSensorController.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    public void AttackEnd()
    {
        attackSensorController.SetActive(false);
        atacking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したゲームオブジェクトのタグを取得
        string tag = other.gameObject.tag;

        // タグをデバッグログに出力
        // Debug.Log("衝突したオブジェクトのタグ: " + tag);

        // 壁に衝突したときの挙動
        if (tag == "Goal")
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("ClearScene");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

// 0待機
// 1移動
// 2ジャンプ
// 3ダッシュ
// 4召喚
// 5咆哮

public class CakeBossControll : MonoBehaviour
{
    // Start is called before the first frame update
    public int state = 0;
    private GameObject player;
    private Transform player_transform;
    private SpriteRenderer renderers;

    // ケーキボスの能力値
    public float movespeed = 5.0f;
    private int max_hp = 25;
    private int hp;
    public float jump_power = 5.0f;
    public float grabity = -10.0f;
    private Animator animator;
    private Rigidbody2D rigid2D;
    private Enemy_se enemy_se;
    private bool houkou_now = false;
    public float moveSpeed = 8f;  // 移動速度
    public float actionInterval = 2f; // アクションの間隔（秒）
    private bool isMoving = false;
    // public GameObject emphasis;
    // private VideoPlayer videoPlayer;
    private bool starthoukou = true;
    private Rigidbody2D rb;
    private float originalGravity = 5.0f;
    private GameObject Gamedirector_ui;
    private GameDirector gameDirector;
    private GameObject healthBar_gameObject;
    private Image healthBar;  //HPバー
    public GameObject mintmoss;
    private GameObject mintmoss_position;
    private GameObject end;
    private End endingScript;
    private GameObject gameObject_bgmcontroller;
    private BGMController bgm_controller;
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar_gameObject = GameObject.Find("boss_hp");
        healthBar = healthBar_gameObject.GetComponent<Image>();
        player = GameObject.Find("player");
        Gamedirector_ui = GameObject.Find("game_director");
        mintmoss_position = GameObject.Find("mintmoss_posi");
        gameObject_bgmcontroller = GameObject.Find("bgm");
        end = GameObject.Find("end");
        endingScript = end.GetComponent<End>();
        player_transform = player.GetComponent<Transform>();
        renderers = GetComponent<SpriteRenderer>();
        hp = max_hp;
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        enemy_se = GetComponent<Enemy_se>();
        // videoPlayer = emphasis.GetComponent<VideoPlayer>();
        rb = GetComponent<Rigidbody2D>();
        gameDirector = Gamedirector_ui.GetComponent<GameDirector>();
        healthBar.type = Image.Type.Filled; // Filled に設定
        healthBar.fillMethod = Image.FillMethod.Horizontal; // Horizontal に設定
        healthBar.fillOrigin = (int)Image.OriginHorizontal.Left; // 左から減るように設定
        UpdateHealthBar();
        bgm_controller = gameObject_bgmcontroller.GetComponent<BGMController>();
    }

    IEnumerator EnemyBehaviorLoop()
    {
        while (true)
        {
            if (hp >= max_hp / 2)
            {
                int action = Random.Range(0, 4); // 0〜3のランダムな動き
                Debug.Log("選ばれたアクション: " + action);

                switch (action)
                {
                    case 0: yield return StartCoroutine(Move()); break;
                    case 1: yield return StartCoroutine(Jump()); break;
                    case 2: yield return StartCoroutine(Idle()); break;
                    case 3: yield return StartCoroutine(ChasePlayer()); break;
                }
                animator.SetInteger("state", 0);
                yield return new WaitForSeconds(actionInterval);
            }
            else
            {
                int action = Random.Range(0, 5); // 0〜4のランダムな動き
                Debug.Log("選ばれたアクション: " + action);

                switch (action)
                {
                    case 0: yield return StartCoroutine(Move()); break;
                    case 1: yield return StartCoroutine(Jump()); break;
                    case 2: yield return StartCoroutine(Idle()); break;
                    case 3: yield return StartCoroutine(ChasePlayer()); break;
                    case 4: yield return StartCoroutine(Summon()); break;
                }
                animator.SetInteger("state", 0);
                yield return new WaitForSeconds(actionInterval);
            }
        }
    }

    // パターン① 左に移動
    IEnumerator Move()
    {
        if (player.transform.position.x < transform.position.x)
        {
            Debug.Log("左に移動");
            animator.SetInteger("state", 1);
            renderers.flipX = false;
            isMoving = true;
            float moveTime = 3f;
            float elapsedTime = 0f;

            while (elapsedTime < moveTime)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isMoving = false;
        }
        else
        {
            Debug.Log("右に移動");
            animator.SetInteger("state", 1);
            renderers.flipX = true;
            isMoving = true;
            float moveTime = 3f;
            float elapsedTime = 0f;

            while (elapsedTime < moveTime)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isMoving = false;
        }
    }

    // パターン③ ジャンプ
    IEnumerator Jump()
    {
        Debug.Log("ジャンプ");
        animator.SetInteger("state", 2);
        Vector3 playerPosition = player.transform.position;  // プレイヤーの位置取得
        float heightOffset = 10f;  // 上空に移動する距離
        float freezeTime = 1f;
        enemy_se.warp();
        transform.position = new Vector3(playerPosition.x, playerPosition.y + heightOffset, this.transform.position.z);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(freezeTime);
        rb.gravityScale = originalGravity;
        animator.SetInteger("state", 0);
        // yield return new WaitForSeconds(0.5f);
    }

    // パターン④ 何もしない（待機）
    IEnumerator Idle()
    {
        Debug.Log("待機");
        animator.SetInteger("state", 0);
        yield return new WaitForSeconds(1f);
    }

    // パターン⑤ プレイヤーを追いかける
    IEnumerator ChasePlayer()
    {
        animator.SetInteger("state", 3);
        Debug.Log("プレイヤーを追跡");
        float chaseTime = 2f;
        float elapsedTime = 0f;
        if (player.transform.position.x < transform.position.x)
        {
            Debug.Log("左に移動");
            animator.SetInteger("state", 1);
            renderers.flipX = false;
            isMoving = true;
            while (elapsedTime < chaseTime)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime * 1.25f;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isMoving = false;
        }
        else
        {
            Debug.Log("右に移動");
            animator.SetInteger("state", 1);
            renderers.flipX = true;
            isMoving = true;
            while (elapsedTime < chaseTime)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime * 1.25f;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isMoving = false;
        }
    }

    // パターン5 召喚
    IEnumerator Summon()
    {
        Houkou();
        Debug.Log("召喚");
        GameObject kobun = Instantiate(mintmoss);
        kobun.transform.position = mintmoss_position.transform.position;
        yield return new WaitForSeconds(3f);
    }

    public void Houkou()
    {
        if (!houkou_now)
        {
            // videoPlayer.Play();
            this.animator.SetInteger("state", 5);
            enemy_se.houkou();
            houkou_now = true;
        }
    }
    public void Houkou_stop()
    {
        // videoPlayer.Stop(); // 停止
        this.animator.SetInteger("state", 0);
        if (starthoukou)
        {
            StartCoroutine(EnemyBehaviorLoop());
            starthoukou = false;
        }
        houkou_now = false;
    }
    private void UpdateHealthBar()
    {
        // 現在のHPに基づいて進行状況バーを更新
        healthBar.fillAmount = (float)hp / max_hp;
        Debug.Log("boss hp = " + hp);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したゲームオブジェクトのタグを取得
        string tag = other.gameObject.tag;
        if (tag == "pl_attack")
        {
            hp -= 1;
            UpdateHealthBar();
            enemy_se.damaged();
            gameDirector.RecoveryMp(1);
            if (hp <= 0)
            {
                Debug.Log("Boss is dead");
                bgm_controller.StopBGM();
                enemy_se.destroy_boss();
                this.animator.SetTrigger("enemy_Death");
                string targetName = "mothmint";
                GameObject[] objectsToDestroy = FindObjectsOfType<GameObject>();
                foreach (GameObject obj in objectsToDestroy)
                {
                    if (obj.name.Contains(targetName))
                    {
                        Destroy(obj);
                    }
                }
                endingScript.endstart();
            }
        }
    }
}
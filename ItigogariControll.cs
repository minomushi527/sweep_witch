using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IchigogariControll : MonoBehaviour
{
    private Animator animator;
    // public GameObject player;

    public float speed = 2f; // 敵キャラクターの移動速度
    public float patrolDistance = 5f; // パトロールの移動範囲

    private Vector3 startPos;
    private bool movingRight = true;

    public int itigogari_hp = 3;

    private Enemy_se enemy_Se;
    public int move = 0;
    private bool isItigoKnockback = false;
    [Header("ノックバック")]
    private Rigidbody2D rb;
    public float knockbackForce = 10.0f;
    public float knockbackTime = 0.5f;
    private GameObject Gamedirector_ui;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("player");
        Gamedirector_ui = GameObject.Find("game_director");
        startPos = transform.position;
        this.animator = GetComponent<Animator>();
        // this.player = GameObject.Find("player");
        enemy_Se = GetComponent<Enemy_se>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (!isItigoKnockback)
        {
            // パトロール範囲での移動
            int move = 0;
            if (movingRight)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                if (transform.position.x <= startPos.x - patrolDistance)
                {
                    movingRight = true;
                }
                move = 1;
            }
            else
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (transform.position.x >= startPos.x + patrolDistance)
                {
                    movingRight = false;
                }
                move = -1;
            }
            if (move != 0)
            {
                transform.localScale = new Vector3(move, 1, 1);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したゲームオブジェクトのタグを取得
        string tag = other.gameObject.tag;
        // タグをデバッグログに出力
        // Debug.Log("衝突したオブジェクトのタグ: " + tag);

        // 壁に衝突したときの挙動
        if (tag == "wall" || tag == "enemy")
        {
            movingRight = !movingRight; // 移動方向を切り替え
        }


        if (tag == "pl_attack")
        {
            if (!isItigoKnockback)
            {
                // プレイヤーと敵キャラの位置からノックバック方向を計算
                StartCoroutine(DamageKnockback());
                // Destroy(this.gameObject);
                itigogari_hp -= 1;
                Debug.Log("itigogari_hp" + itigogari_hp);
                enemy_Se.damaged();
                GameDirector gameDirector = Gamedirector_ui.GetComponent<GameDirector>();
                gameDirector.RecoveryMp(1);
                if (itigogari_hp == 0)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<Rigidbody2D>().simulated = false;
                    gameDirector.RecoveryMp(1);
                    animator.SetTrigger("enemy_Death");
                }
            }
        }
    }
    //イチゴガリのノックバック処理
    private IEnumerator DamageKnockback()
    {
        isItigoKnockback = true;

        // ノックバック力を加える
        if (this.transform.position.x < player.transform.position.x)
        {
            rb.AddForce(Vector3.up * knockbackForce * 0.1f, ForceMode2D.Impulse);
            rb.AddForce(Vector3.left * knockbackForce * 3, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.up * knockbackForce * 0.1f, ForceMode2D.Impulse);
            rb.AddForce(Vector3.right * knockbackForce * 3, ForceMode2D.Impulse);
        }
        // ノックバック後、指定した時間だけ動かす
        yield return new WaitForSeconds(knockbackTime * Time.deltaTime * 60);

        // ノックバック終了後、速度をゼロに戻す
        // rb.velocity = Vector2.zero;
        isItigoKnockback = false;
        Debug.Log("knockback");
    }

    void destroy_me()
    {
        Destroy(this.gameObject);
    }
}

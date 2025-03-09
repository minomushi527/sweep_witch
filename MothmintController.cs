using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothmintController : MonoBehaviour
{
    private GameObject player;
    // プレイ��ーを格��するオブジェクト
    private Transform player_transform; // プレイヤーのTransformを指定する
    public float moveSpeed = 4.0f; // 敵の移動速度
    public float stoppingDistance = 1.0f; // プレイヤーとの最小距離
    public float ExploreDistance = 20.0f; //探索距離
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public int mint_hp = 5;
    private Enemy_se enemy_Se;
    private bool isItigoKnockback = false;
    private Rigidbody2D rb;
    public float knockbackForce = 10.0f;
    public float knockbackTime = 0.5f;
    private GameObject Gamedirector_ui;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        player = GameObject.Find("player");
        Gamedirector_ui = GameObject.Find("game_director");
        player_transform = player.GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy_Se = GetComponent<Enemy_se>();
        rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }
    void Update()
    {
        // プレイヤーとの距離を計算
        float distanceToPlayer = Vector3.Distance(transform.position, player_transform.position);

        // プレイヤーとの距離がstoppingDistanceより大きく探索範囲より小さい場合、近づく
        if (distanceToPlayer > stoppingDistance && distanceToPlayer < ExploreDistance)
        {
            Vector3 direction = (player_transform.position - transform.position).normalized;

            // 右方向に向かっている場合は反転
            if (direction.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
    private IEnumerator DamageKnockback()
    {
        isItigoKnockback = true;

        // ノックバック力を加える
        if (this.transform.position.x < player.transform.position.x)
        {
            rb.AddForce(Vector3.left * knockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * knockbackForce, ForceMode2D.Impulse);
        }
        // ノックバック後、指定した時間だけ動かす
        yield return new WaitForSeconds(knockbackTime * Time.deltaTime * 60);

        // ノックバック終了後、速度をゼロに戻す
        rb.velocity = Vector2.zero;
        isItigoKnockback = false;
        Debug.Log("knockback");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したゲームオブジェクトのタグを取得
        string tag = other.gameObject.tag;
        // タグをデバッグログに出力
        // Debug.Log("衝突したオブジェクトのタグ: " + tag);

        if (tag == "pl_attack")
        {
            if (!isItigoKnockback)
            {
                // プレイヤーと敵キャラの位置からノックバック方向を計算
                StartCoroutine(DamageKnockback());
                // Destroy(this.gameObject);
                mint_hp -= 1;
                Debug.Log("mint_hp" + mint_hp);
                enemy_Se.damaged();
                GameDirector gameDirector = Gamedirector_ui.GetComponent<GameDirector>();
                gameDirector.RecoveryMp(1);
                if (mint_hp == 0)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                    GetComponent<Rigidbody2D>().simulated = false;
                    gameDirector.RecoveryMp(1);
                    animator.SetTrigger("enemy_Death");
                }
            }
        }
    }
    void destroy_me()
    {
        Destroy(this.gameObject);
    }
}

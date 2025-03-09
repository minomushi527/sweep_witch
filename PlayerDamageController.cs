using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public bool isDamageCooldown = false;//無敵時間判定(trueの間は無敵)
    public GameObject Gamedirector_ui;
    public bool isKnockbackchild;
    public GameObject player;
    private PlayerController playerController;
    [SerializeField] Animator animator;
    [Header("ノックバック")]
    public float knockbackTime;//ノックバックする時間
    public float knockbackForce;//ノックバックの威力
    [Header("無敵時間")]
    public float muteki;
    [Header("受けるダメージ量")]
    public int itigogariDamage = 1;//イチゴガリが与えるダメージ
                                   //この下にこの形でダメージを追加していく
    public int fallenDamage = 1;
    public int cakeDamage = 1; //ケーキボスが与えるダメージ
    public int mothmintDamage = 1; //モスミントが与えるダメージ

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }


    //PlayerDamageController内で受けたダメージ計算、反映もさせるようにしました。
    //これから先敵が増えていく中で、こっちの方が調整しやすいと思ったからです。
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            playerController.AttackEnd();
            if (!isDamageCooldown && !isKnockbackchild)
            {
                StartCoroutine(DamageKnockback(col.gameObject));

                string enemyName = col.gameObject.name;
                if (enemyName.Contains("itigogari"))
                {
                    StartCoroutine(DamageCoroutine(itigogariDamage));
                }
                //敵が増えた場合、その都度else if文をこの下に追加していく
                //例)else if (enemyName == "敵のゲームオブジェクトの名前")
                //{
                //  StartCoroutine(DamageCoroutine(敵の名前+Damage->これは随時int型変数として追加する));
                //}
                else if (enemyName.Contains("cakeboss"))
                {
                    StartCoroutine(DamageCoroutine(cakeDamage));
                }
                else if (enemyName.Contains("mothmint"))
                {
                    StartCoroutine(DamageCoroutine(mothmintDamage));
                }


                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.animator.SetTrigger("DamagedTrigger_L");
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.animator.SetTrigger("DamagedTrigger_R");
                }
                else
                {
                    AnimatorStateInfo stateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
                    if (stateInfo.IsName("pl_run_R") || stateInfo.IsName("pl_stand_R") || stateInfo.IsName("pl_jump_R"))
                    {
                        this.animator.SetTrigger("DamagedTrigger_R");
                    }
                    if (stateInfo.IsName("pl_run_L") || stateInfo.IsName("pl_stand_L") || stateInfo.IsName("pl_jump_L"))
                    {
                        this.animator.SetTrigger("DamagedTrigger_L");
                    }
                }
            }
        }
        // 落下時ダメージ1
        if (col.gameObject.tag == "fall")
        {
            playerController.AttackEnd();
            if (!isDamageCooldown)
            {
                StartCoroutine(DamageCoroutine(fallenDamage));
            }
        }
    }

    IEnumerator DamageCoroutine(int damage)
    {
        // �N�[���_�E�����J�n
        isDamageCooldown = true;

        // �_���[�W����
        GameDirector gameDirector = Gamedirector_ui.GetComponent<GameDirector>();
        gameDirector.DecreaseHp(damage);

        // 2�b�ԑҋ@
        yield return new WaitForSeconds(muteki);

        // �N�[���_�E�����I��
        isDamageCooldown = false;
    }

    IEnumerator DamageKnockback(GameObject obj)
    {
        isKnockbackchild = true;
        // 自身とobjの位置を取得
        Vector2 myPosition = transform.parent.position; //プレイヤーの位置
        Vector2 objPosition = obj.transform.position; //objの位置

        // ノックバック方向を計算
        Vector2 knockbackDirection = (myPosition - objPosition).normalized;
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackTime);
        isKnockbackchild = false;
    }
}



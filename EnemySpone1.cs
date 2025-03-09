using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpone1 : MonoBehaviour
{
    public GameObject EnemyBirthControll;
    private EnemyBirth enemyBirth;
    // Start is called before the first frame update
    void Start()
    {
        enemyBirth = EnemyBirthControll.GetComponent<EnemyBirth>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突したゲームオブジェクトのタグを取得
        string tag = other.gameObject.tag;

        // タグをデバッグログに出力
        // Debug.Log("衝突したオブジェクトのタグ: " + tag);

        // 壁に衝突したときの挙動
        if (tag == "Player")
        {
            enemyBirth.DestroyEnemy();
            enemyBirth.CreateEnemy2();
            Destroy(this.gameObject);
        }
    }
}

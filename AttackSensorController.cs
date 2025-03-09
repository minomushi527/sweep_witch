using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSensorController : MonoBehaviour
{
    // private Rigidbody2D rd;                  // Rigidbody2D の参照
    // public GameObject target;               // 回転の中心オブジェクト
    // public Collider2D attackcollider;             // 攻撃用コライダー
    // public Transform player;                // プレイヤーのTransform
    // public Vector3 rotationAxis = new Vector3(0, 0, 1); // 回転軸 (例: Z軸)
    // public float rotationAngle = 90f;       // 回転する角度
    // public float duration = 0.45f;          // 回転にかける時間
    // public float delayBeforeRotation = 0.03f; // 回転を開始するまでの遅延時間

    // private Quaternion startRotation;       // 初期の回転
    // private Quaternion targetRotation;      // 目標の回転
    // private float elapsedTime = 0f;         // 経過時間
    // private bool isRotating = false;        // 回転中かどうか

    public GameObject atarihantei;
    private Collider2D attackcollider;

    void Start()
    {
        // rd = GetComponent<Rigidbody2D>();
        attackcollider = atarihantei.GetComponent<Collider2D>();

        if (attackcollider != null)
        {
            attackcollider.enabled = false;       // コライダーを無効化
        }
        // startRotation = transform.rotation; // 初期の回転を記録
    }

    void Update()
    {
        // // 回転中の処理
        // if (isRotating)
        // {
        //     elapsedTime += Time.deltaTime;
        //     float t = elapsedTime / duration;
        //     transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

        //     // 回転が終了したら停止し、向きをリセット
        //     if (elapsedTime >= duration)
        //     {
        //         transform.rotation = startRotation; // 初期の回転にリセット
        //         isRotating = false;
        //     }
        // }

        // // Zキーで回転を開始 (0.03秒後に実行)
        // if (Input.GetKeyDown(KeyCode.Z) && !isRotating)
        // {
        //     Invoke(nameof(StartRotationWithDelay), delayBeforeRotation);
        // }

        // Zキーでコライダーを切り替え
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (attackcollider != null)
            {
                attackcollider.enabled = true;
            }
        }
        else
        {
            if (attackcollider != null)
            {
                attackcollider.enabled = false;
            }
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Destroy(gameObject); // 攻撃オブジェクトの破棄
    // }

    // private void StartRotationWithDelay()
    // {
    //     if (target != null && player != null)
    //     {
    //         // プレイヤーの向きに基づいて回転方向を変更
    //         Vector3 playerDirection = player.transform.localScale.x > 0 ? Vector3.forward : Vector3.back;
    //         rotationAxis = playerDirection;

    //         StartRotation();
    //     }
    // }

    // private void StartRotation()
    // {
    //     isRotating = true;
    //     elapsedTime = 0f;
    //     startRotation = transform.rotation; // 現在の回転を初期回転として記録

    //     // 目標回転を計算
    //     Quaternion rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis);
    //     targetRotation = startRotation * rotation;
    // }
}


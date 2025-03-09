using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    private SpriteRenderer spriteRenderer; // スプライトのレンダラー

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // スプライト取得
    }

    void Update()
    {
        if (player != null)
        {
            // プレイヤーの x 座標と比較
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true; // 右向き（反転）
            }
            else
            {
                spriteRenderer.flipX = false; // 左向き（通常）
            }
        }
    }
}

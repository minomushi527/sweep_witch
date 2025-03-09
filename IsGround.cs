using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGround : MonoBehaviour
{
    public bool isGrounded = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))  // トリガーが地面オブジェクトと接触した場合
        {
            isGrounded = true;  // 地面に足がついているとする
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))  // トリガーが地面オブジェクトとの接触が終了した場合
        {
            isGrounded = false;  // 地面に足がついていないとする
        }
    }
}

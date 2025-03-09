using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallenhelp : MonoBehaviour
{
    // 大文字(A)で受け止めて、小文字(a)に持っていく
    public GameObject warp_to_here;
    public GameObject player;
    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = warp_to_here.transform.position;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // オブジェクトにPlayerタグが付いているか確認
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player is coming");
            player.transform.position = targetPosition;
        }
        if (collision.CompareTag("enemy"))
        {
            // タグが"Enemy"ならそのオブジェクトを削除
            Destroy(collision.gameObject);
        }
    }
}

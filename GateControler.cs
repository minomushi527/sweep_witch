using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControler : MonoBehaviour
{
    public int applecandles = 4;
    public AudioSource audiosource;
    public AudioClip tojiru_oto;
    public GameObject bat;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        if (audiosource == null)
        {
            audiosource = gameObject.AddComponent<AudioSource>();
        }
    }


    public void Fire_kesu()
    {
        --applecandles;
        if (applecandles == 0)
        {
            MoveGate();
        }
    }
    public void MoveGate()
    {
        // 現在の位置を取得
        Vector3 currentPosition = transform.position;

        // 自身の大きさを取得 (高さはlocalScale.y)
        float sizeY = transform.localScale.y;

        // 新しい位置を計算 (y座標に大きさ分を加算)
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y + (sizeY * 5), currentPosition.z);

        // 新しい位置を設定
        transform.position = newPosition;
        Destroy(bat);
        PlayTojiruOto();
    }

    public void RemoveGate()
    {
        // 現在の位置を取得
        Vector3 currentPosition = transform.position;

        // 自身の大きさを取得 (高さはlocalScale.y)
        float sizeY = transform.localScale.y;

        // 新しい位置を計算 (y座標に大きさ分を加算)
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y - (sizeY * 5), currentPosition.z);

        // 新しい位置を設定
        transform.position = newPosition;
    }
    public void PlayTojiruOto()
    {
        audiosource.PlayOneShot(tojiru_oto);
    }

}

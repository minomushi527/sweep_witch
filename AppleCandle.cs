using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCandle : MonoBehaviour
{
    public Sprite nonfire_candle;
    public GameObject gate;
    private GateControler gateControler;
    public AudioClip syouka;
    private AudioSource audioSource;
    private bool fire;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gateControler = gate.GetComponent<GateControler>();
        fire = true;
    }
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D other)
    {
        if (fire)
        {
            // 衝突したゲームオブジェクトのタグを取得
            string tag = other.gameObject.tag;
            if (tag == "pl_attack")
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = nonfire_candle;
                audioSource.PlayOneShot(syouka);
                gateControler.Fire_kesu();
                fire = false;
            }
        }
    }
}

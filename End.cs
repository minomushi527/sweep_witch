using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private GameObject cake;
    public GameObject player;
    // public GameObject clearCanvas;
    public MonoBehaviour playerScript;
    private MonoBehaviour cakeScript;
    public GameObject bgm;
    private BGMController controller;
    private SpriteRenderer spriteRenderer;
    private Color objectColor;
    private float fadeSpeed = 1f; // 透明度が増していくスピード
    public GameObject cake_hp_ui;
    public GameObject enemybirth;
    private EnemyBirth birthScript;
    public GameObject fireflower;

    public void endstart()
    {
        cake = GameObject.Find("cakeboss(Clone)");
        if (cake == null)
        {
            cake = GameObject.Find("cakeboss");
        }
        controller = bgm.GetComponent<BGMController>();
        if (cake != null)
        {
            cakeScript = cake.GetComponent<CakeBossControll>();
            cakeScript.enabled = false;
            spriteRenderer = cake.GetComponent<SpriteRenderer>(); // SpriteRendererを取得
            objectColor = spriteRenderer.color; // 初期カラーを取得
        }
        else
        {
            Debug.LogWarning("cake is not floundable");
        }
        birthScript = enemybirth.GetComponent<EnemyBirth>();
        playerScript.enabled = false;
        StartCoroutine(Ending()); // コルーチンを開始
    }

    IEnumerator Ending()
    {
        float time = 0.0f;
        if (cake != null)
        {
            while (time < 2.0f)
            {
                if (cake != null)
                {
                    if (objectColor.a > 0)
                    {
                        // 透明度が0より大きい場合
                        objectColor.a -= fadeSpeed * Time.deltaTime; // アルファ値を減らす
                        objectColor.a = Mathf.Clamp01(objectColor.a); // 0以下にならないようにする
                        spriteRenderer.color = objectColor; // カラーを更新
                    }
                    time += 0.1f;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            Destroy(cake);
        }
        birthScript.DestroyEnemy();
        cake_hp_ui.SetActive(false);
        yield return new WaitForSeconds(1f);
        controller.PlayEndingBgm();
        Instantiate(fireflower, new Vector3(player.transform.position.x - 5.0f, player.transform.position.y, player.transform.position.z), Quaternion.identity);
        Instantiate(fireflower, new Vector3(player.transform.position.x + 11.0f, player.transform.position.y, player.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ClearScene");
    }
}
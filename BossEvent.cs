using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    private bool isCounting = false; // イベント発生するかどうか
    private float timer = 0.0f; // タイマー
    public GameObject gameObject_bgmcontroller;
    private BGMController controller;
    private bool boss_start = false;
    public GameObject player;
    public MonoBehaviour playerScript;
    private Animator animator;
    public GameObject gate;
    private GateControler gateScript;
    private Vector3 gatePos;
    private int state;
    public GameObject boss_prefab;
    private CakeBossControll cakeboss_controller;
    public MonoBehaviour cameraScript;

    public GameObject ui_parent;
    public GameObject ui_bgm_name;
    private RectTransform ui_bgm_name_transform;
    private Vector3 targetPosition;
    private GameObject boss;
    [SerializeField] GameObject fullrecovery;
    [SerializeField] GameObject fullrecovery_warppoint;

    void Start()
    {
        // BGMコントローラーを取得する
        controller = gameObject_bgmcontroller.GetComponent<BGMController>();
        animator = player.GetComponent<Animator>();
        gateScript = gate.GetComponent<GateControler>();
        gatePos = gate.transform.position;
        state = 0;
        ui_bgm_name_transform = ui_bgm_name.GetComponent<RectTransform>();
        targetPosition = new Vector3(256f, -304f, 0f);
    }
    void Update()
    {
        if (isCounting)
        {
            timer += Time.deltaTime;
        }
        if (boss_start)
        {
            bossevent();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss_start = true;
        }
    }
    void bossevent()
    {
        //0
        // プレイヤーが停止、BGM停止
        if (state == 0)
        {
            fullrecovery.transform.position = fullrecovery_warppoint.transform.position;
            isCounting = true;
            controller.StopBGM();
            playerScript.enabled = false; // プレイ��ーを動かなくする
            // animator.Play("Base Layer.pl_stand_R", 0, 0f);
            animator.SetBool("runBool_R", false);
            state = 1;
        }

        // 閉じる音再生
        if (state == 1 && timer > 0.5f)
        {
            gateScript.PlayTojiruOto();
            state = 2;
            // 敵の削除
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        if (state == 2 && timer > 0.5f)
        {
            gate.transform.position += new Vector3(0, -5.0f * Time.deltaTime, 0);
            if (gate.transform.position.y <= gatePos.y)
            {
                state = 3;
                timer = 0.0f;
            }
        }
        if (state == 3 && timer > 1.0f)
        {
            //ボス出現
            boss = Instantiate(boss_prefab, new Vector3(324, 10, -1), Quaternion.identity);
            cakeboss_controller = boss.GetComponent<CakeBossControll>();
            cameraScript.enabled = false;
            if (ui_parent != null)
            {
                ui_parent.SetActive(true); // UIを表示
            }
            state = 4;
        }
        if (state == 4 && timer > 1.0f)
        {
            Camera.main.transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y, -10.0f);
            if (timer > 3.0f)
            {
                state = 5;
            }
        }
        if (state == 5 && timer > 3.5f)
        {
            cakeboss_controller.Houkou(); //咆哮
            state = 6;
        }
        if (state == 6 && timer > 4.5f)
        {
            controller.PlayBossBgm(); // BGMをボスBGMに切り替える
            cameraScript.enabled = true;
            playerScript.enabled = true;
            ui_bgm_name.SetActive(true);
            state = 7;
        }
        if (state == 7 && timer > 5.5f)
        {
            ui_bgm_name.SetActive(true);
            if (timer > 8.0f)
            {
                ui_bgm_name.SetActive(false);
                Destroy(gameObject); // イベント発生したら自身を消去する。}
            }
        }
    }
}

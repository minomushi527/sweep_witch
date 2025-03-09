using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GameObject player;
    public GameObject mpCase;
    public GameObject lifePrefab;
    public Transform uiParent;
    private float x_distance = 100.0f;
    private float x_syokiti = 378.0f;
    private float y_syokiti = 101.0f;
    private int max_hp;
    private int max_mp;
    public GameObject[] lifes;

    // public PlayerStatus playerStatus;
    // Start is called before the first frame updates
    private void Start()
    {
        // 最大HPが変わった時再度呼び出すこと
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        max_hp = playerdatamanager.playerStatus.max_hp;
        max_mp = playerdatamanager.playerStatus.max_mp;
        lifes = new GameObject[max_hp];
        // Debug.Log("hp:" + hp);

        GameObject life;
        for (int i = 0; i < max_hp; i++)
        {
            life = Instantiate(lifePrefab, uiParent);
            life.GetComponent<RectTransform>().anchoredPosition = new Vector3((x_syokiti + x_distance * i), y_syokiti);
            // Debug.Log($"ライフ作成" + (x_syokiti + x_distance * i));
            lifes[i] = life;
        }

    }
    // Update is called once per frame
    // 引数には食らったダメージor回復したhpを入れる
    // public void DecreaseHp()
    // {

    // }

    //エラーが出ていたようなので、関数を少し修正しました。赤文字のデバックログは死のカウントダウンです。
    //関数の中身のことで質問があれば気楽に石田まで！
    public void DecreaseHp(int decrease_hp)
    {
        Debug.Log("ダメージ");
        Animator animator;
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        int pre_hp = playerdatamanager.playerStatus.hp;
        playerdatamanager.playerStatus.hp -= decrease_hp;
        if (playerdatamanager.playerStatus.hp <= 0)
        {
            animator = lifes[0].GetComponent<Animator>();
            bool currentLife = animator.GetBool("life");
            animator.SetBool("life", false);
            Debug.Log("Death!!");
            return;
        }
        for (int i = pre_hp - 1; i > (pre_hp - 1) - decrease_hp; i--)
        {
            Debug.Log($"<color=red>{i}</color>");
            animator = lifes[i].GetComponent<Animator>();
            bool currentLife = animator.GetBool("life");
            if (currentLife == true)
            {
                animator.SetBool("life", false);
            }
        }
    }
    public void RecoveryHp(int recovery_hp)
    {
        Animator animator;
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        int pre_hp = playerdatamanager.playerStatus.hp;
        int max_hp = playerdatamanager.playerStatus.max_hp;
        //回復hpが最大hp以上になる場合
        if (pre_hp + recovery_hp >= max_hp)
        {
            if (pre_hp == 0)
            {
                playerdatamanager.playerStatus.hp = max_hp;
                for (int i = 0; i < recovery_hp; i++)
                {
                    if (i < lifes.Length)
                    {
                        animator = lifes[i].GetComponent<Animator>();
                        bool currentLife = animator.GetBool("life");
                        if (currentLife == false)
                        {
                            animator.SetBool("life", true);
                        }
                    }
                }
                return;
            }
            else
            {
                for (int i = pre_hp - 1; i < max_hp; i++)
                {
                    if (i < lifes.Length)
                    {
                        animator = lifes[i].GetComponent<Animator>();
                        bool currentLife = animator.GetBool("life");
                        if (currentLife == false)
                        {
                            animator.SetBool("life", true);
                        }
                    }
                }
                playerdatamanager.playerStatus.hp = max_hp;
                return;
            }
        }
        else
        {
            playerdatamanager.playerStatus.hp += recovery_hp;
            //すでに体力が0の場合
            if (pre_hp == 0)
            {
                for (int i = 0; i < recovery_hp; i++)
                {
                    if (i < lifes.Length)
                    {
                        animator = lifes[i].GetComponent<Animator>();
                        bool currentLife = animator.GetBool("life");
                        if (currentLife == false)
                        {
                            animator.SetBool("life", true);
                        }
                    }
                }
                return;
            }
            else
            {
                for (int i = pre_hp - 1; i < (pre_hp - 1) + recovery_hp; i++)
                {
                    if (i < lifes.Length)
                    {
                        animator = lifes[i].GetComponent<Animator>();
                        bool currentLife = animator.GetBool("life");
                        if (currentLife == false)
                        {
                            animator.SetBool("life", true);
                        }
                    }
                }
                playerdatamanager.playerStatus.hp = max_hp;
                return;
            }
        }

    }
    public void DecreaseMp(int decrease_mp)
    {
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        Debug.Log($"mp消費" + playerdatamanager.playerStatus.mp);
        playerdatamanager.playerStatus.mp -= decrease_mp;
        UI_mpControll mpControll = mpCase.GetComponent<UI_mpControll>();
        mpControll.CheckMP();
    }


    public void RecoveryallMp()
    {
        Debug.Log("mp全回復");
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        playerdatamanager.playerStatus.mp = max_mp;
        UI_mpControll mpControll = mpCase.GetComponent<UI_mpControll>();
        mpControll.CheckMP();
    }
    public void RecoveryMp(int recovery_mp)
    {
        Debug.Log("mp回復");
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        if (playerdatamanager.playerStatus.max_mp >= recovery_mp + playerdatamanager.playerStatus.mp)
        {
            playerdatamanager.playerStatus.mp += recovery_mp;
        }
        UI_mpControll mpControll = mpCase.GetComponent<UI_mpControll>();
        mpControll.CheckMP();

    }

    public bool Flyok()
    {
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        if (playerdatamanager.playerStatus.mp > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

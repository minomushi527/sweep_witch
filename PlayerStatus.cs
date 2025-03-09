using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
// [CreateAssetMenu(fileName = "PlayerStatus", menuName = "Game/PlayerStatus", order = 1)]
// : ScriptableObject
public class PlayerStatus
{
    public int ido;
    public int hp;
    public int max_hp;
    public int mp;
    public int max_mp;
    public int candy;

    // private PlayerStatus instance;

    // public static PlayerStatus Instance
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = new PlayerStatus();
    //         }
    //         return _instance;
    //     }
    // }

    public PlayerStatus(int ido, int hp, int max_hp, int mp, int max_mp, int candy)
    {
        this.ido = ido;
        this.hp = hp;
        this.max_hp = max_hp;
        this.mp = mp;
        this.max_mp = max_mp;
        this.candy = candy;
    }
    public PlayerStatus()
    {
        this.ido = 0;
        this.hp = 6;
        this.max_hp = 6;
        this.mp = 14;
        this.max_mp = 14;
        this.candy = 0;
    }

    // public GameObject[] lifeArray = new GameObject[3];
    // private int lifePoint = 3;

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0) && lifePoint < 3)
    //     {
    //         lifePoint++;
    //         lifeArray[lifePoint - 1].SetActive(true);
    //     }

    //     else if (Input.GetMouseButtonDown(1) && lifePoint > 0)
    //     {
    //         lifeArray[lifePoint - 1].SetActive(false);
    //         lifePoint--;
    //     }
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_mpControll : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public GameObject player;
    public Sprite mp0;
    public Sprite mp1;
    public Sprite mp2;
    public Sprite mp3;
    public Sprite mp4;
    public Sprite mp5;
    public Sprite mp6;
    public Image uiImage;

    private int maxmp;
    private float memori;


    // Start is called before the first frame update
    void Start()
    {
        // 最大MPが変わったら再度呼び出し
        uiImage = GetComponent<Image>();
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        maxmp = playerdatamanager.playerStatus.max_mp;
        memori = maxmp / 7;
    }

    // Update is called once per frame
    public void CheckMP()
    {
        PlayerDataManager playerdatamanager = player.GetComponent<PlayerDataManager>();
        int mp = playerdatamanager.playerStatus.mp;
        if (0 <= mp && mp < memori * 1)
        {
            uiImage.sprite = mp0;
        }
        else if (memori * 1 <= mp && mp < memori * 2)
        {
            uiImage.sprite = mp1;
        }
        else if (memori * 2 <= mp && mp < memori * 3)
        {
            uiImage.sprite = mp2;
        }
        else if (memori * 3 <= mp && mp < memori * 4)
        {
            uiImage.sprite = mp3;
        }
        else if (memori * 4 <= mp && mp < memori * 5)
        {
            uiImage.sprite = mp4;
        }
        else if (memori * 5 <= mp && mp < memori * 6)
        {
            uiImage.sprite = mp5;
        }
        else
        {
            uiImage.sprite = mp6;
        }
    }
}

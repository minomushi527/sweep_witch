using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] menuOptions;  // UIのテキスト（選択肢）
    public GameObject selector; // ダイヤマークの画像（UI）
    public GameObject instructionImage; // 操作説明の画像

    private int selectedIndex = 0;  // 選択中の項目
    private bool isInstructionOn = false; // ��作説明画面が表示されているかどうか

    private AudioSource audiosource;
    public AudioClip cursormovement_se; //カーソル移動のse
    public AudioClip select_se; //決定時のse
    public AudioClip cancel_se; //キャンセル時のse

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        UpdateSelectorPosition();
        instructionImage.SetActive(false); // 最初は操作説明を非表示
    }

    void Update()
    {
        if (!isInstructionOn)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (selectedIndex > 0)
                {
                    audiosource.PlayOneShot(cursormovement_se);
                    selectedIndex -= 1;
                    UpdateSelectorPosition();
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (selectedIndex < menuOptions.Length - 1)
                {
                    audiosource.PlayOneShot(cursormovement_se);
                    selectedIndex += 1;
                    UpdateSelectorPosition();
                }
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                audiosource.PlayOneShot(select_se);
                ExecuteSelection();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                audiosource.PlayOneShot(cancel_se);
                instructionImage.SetActive(false);
                isInstructionOn = false;
            }
        }
    }

    void UpdateSelectorPosition()
    {
        // ダイヤの位置を選択中のテキストと同じ y座標にする
        Vector3 newPosition = selector.transform.position;
        newPosition.y = menuOptions[selectedIndex].transform.position.y;
        selector.transform.position = newPosition;
    }

    void ExecuteSelection()
    {
        switch (selectedIndex)
        {
            case 0: // はじめる
                SceneManager.LoadScene("CakeStage"); // ゲームのシーン名に変更
                break;
            case 1: // 操作説明
                instructionImage.SetActive(true);
                isInstructionOn = true;
                break;
            case 2: // おわる
                Application.Quit();
                break;
        }
    }
}

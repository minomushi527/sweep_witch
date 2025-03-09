using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Bad2 : MonoBehaviour
{
    public GameObject textbox; // イベント発生するオブジェクト
    public GameObject text;
    private TextMeshProUGUI messageText; // UI のテキスト
    private string[] sentences = {
        "うーん、この先の扉を開くにはどうやら\n4つのロウソクを消さないといけないようだね。",
        "のこりのロウソクは上にあったよ。\n落ちないように気を付けてね。",
        "まあ君は小さくとも立派な魔女だから、\n落ちたところでホウキに乗って上れるか。",
        "覚えているかい？\nshiftを押したらホウキに乗れるから、そこですかさず上ボタンだ！"
    };  // セリフ一覧
    private int index = 0;      // 現在のセリフ番号
    private bool isTyping = false; // 文字を表示中かどうか
    private bool text_on = false; // 吹き出しが表示されているかどうか
    public float typingSpeed = 0.05f; // 文字の表示速度

    void Start()
    {
        messageText = text.GetComponent<TextMeshProUGUI>(); // UI のテキストコンポー��ントを取得
        if (messageText == null)
        {
            Debug.LogError("Text component is missing!");
        }
        textbox.SetActive(false); // 最初は非表示
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // プレイヤーがエリアに入ったら
        {
            textbox.SetActive(true);
            text_on = true;
            if (!isTyping)
            {
                StartCoroutine(TypeSentence(sentences[index])); // 最初のセリフを表示
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // プレイヤーがエリアを出たら
        {
            if (text_on)
            {
                textbox.SetActive(false);
                text_on = false;
                index = 0; // 最初のセリフに戻す
            }
        }
    }

    void Update()
    {
        if (text_on && !isTyping && Input.GetKeyDown(KeyCode.X))
        {
            NextSentence();
        }
    }

    void NextSentence()
    {
        index++;
        if (index < sentences.Length)
        {
            StartCoroutine(TypeSentence(sentences[index]));
        }
        else
        {
            // messageText.SetActive(false); // 全部表示したら非表示に
            textbox.SetActive(false);
            text_on = false;
            index = 0; // 最初のセリフに戻す
        }
    }
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        messageText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 一文字ずつ表示
        }

        isTyping = false;
    }

}



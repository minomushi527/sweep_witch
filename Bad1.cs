using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Bad1 : MonoBehaviour
{
    public GameObject textbox; // イベント発生するオブジェクト
    public GameObject text;
    private TextMeshProUGUI messageText; // UI のテキスト
    private string[] sentences = {
        "やあ、そこの小さな魔女ちゃん。ご機嫌いかがかな？",
        "困惑しているようだね。無理もない。\n君は覚えていないようだから、\nここに来るまでのことをおさらいしておこうか。",
        "一人おるすばんをしていたら鏡が光って、\n触れると君は「こちら側」に吸い込まれてしまった。",
        "そこは不思議な魔法の世界。\nケーキの地面に動くいちご、おまけに喋るコウモリときた。",
        "ワーオ、夢があるね！  それもそうか、だってネタバレすると",
        "ここは君の夢の中なんだから。",
        "そのしかめっ面を見るに、ケーキの世界はお気に召さなかったかな。\n子供なら一度は夢見る楽園だと思うけれど。",
        "まあ、そりゃそうか。だって君はケーキが大嫌いなんだから。\nクリスマスも誕生日も大嫌い。",
        // "クリスマスであり君の誕生日でもある大事な日に、\n君のおかあさんは帰ってこなかった。",
        // "確かに一人で帰りを待つのは心細かっただろう。\nでもいつまでも引きずることじゃない。\n次の日の朝にはちゃあんと無事に帰ってきたじゃないか。",
        // "しかも君のためにケーキまで買ってきて！\nでも君は、せっかくのケーキを吐いてしまった。\nどうしてだい。",
        // "朝ごはんにケーキは流石に重かったか！\nクリームがくどかったよね。",
        // "それとも半額のコンビニケーキは口に合わなかったかな。\nそれともおかあさんから漂う知らない香水の匂いに酔っちゃった？",
        "大丈夫、夢の中では全てが思うまま！自由自在さ。",
        "君が夢に見るまでに何を引きずっているかは知らないけれど、\nキライな物はぜーんぶ",
        "ホウキではいて片づけてしまえばいいんだ！\n掃除は得意だろう？",
        "さあ行きな、ホウキをもった小さな魔女ちゃん。\nモヤモヤする物は全て押し流してしまえば、\nきっとスッキリするはずさ！"
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



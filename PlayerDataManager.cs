using UnityEngine;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject FullRecovery;
    [SerializeField] GameDirector gameDirector;

    public PlayerStatus playerStatus = new PlayerStatus();

    private void Awake()
    {
        // // シングルトンパターンの設定
        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject); // シーン遷移後もオブジェクトを保持
        // }
        // else
        // {
        //     Destroy(gameObject); // 既に存在する場合は新たに作らない
        // }
    }

    private void Update()
    {
        if (playerStatus.hp <= 0)
        {
            Player.transform.position = new Vector3(FullRecovery.transform.position.x, FullRecovery.transform.position.y, FullRecovery.transform.position.z - 1);
            gameDirector.RecoveryHp(playerStatus.max_hp);
        }
    }


    //////////////////////////////////////////////////////////////////////////////////////////

    // セーブ機能
    public void SaveData()
    {
        // string jsonData = JsonUtility.ToJson(playerStatus); // JSONに変換
        // File.WriteAllText(Application.persistentDataPath + "/playerData.json", jsonData);
    }

    // ロード機能
    public void LoadData()
    {
        // string filePath = Application.persistentDataPath + "/playerData.json";
        // if (File.Exists(filePath))
        // {
        //     string jsonData = File.ReadAllText(filePath);
        //     playerStatus = JsonUtility.FromJson<PlayerStatus>(jsonData); // JSONから復元
        // }
        // else
        // {
        //     InitializeStatus(); // ファイルがない場合は初期化
        // }
    }
}

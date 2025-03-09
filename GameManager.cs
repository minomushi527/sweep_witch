using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        PlayerDataManager.Instance.LoadData(); // ゲーム開始時にデータをロード
    }

    private void OnApplicationQuit()
    {
        PlayerDataManager.Instance.SaveData(); // ゲーム終了時にデータをセーブ
    }
}

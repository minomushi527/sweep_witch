using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverrySpotController : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    [SerializeField] PlayerDataManager playerDataManager;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("回復");
            gameDirector.RecoveryHp(playerDataManager.playerStatus.max_hp);
            gameDirector.RecoveryallMp();
        }
    }
}

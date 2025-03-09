using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lake_mprecovery : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    [SerializeField] PlayerDataManager playerDataManager;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("回復");
            gameDirector.RecoveryallMp();
        }
    }
}

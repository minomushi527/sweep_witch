using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirth : MonoBehaviour
{
    public GameObject itigogariPrefab;
    public GameObject mintmossPrefab;
    private struct EnemyPosition
    {
        public float x, y;
        public EnemyPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    private List<EnemyPosition> Ichigogari_enemyPosition1 = new List<EnemyPosition>{
        new EnemyPosition(34.0f,-2.8f),
        new EnemyPosition(205.0f, 0.0f),
        new EnemyPosition(70.0f, 7.0f),
        new EnemyPosition(126.0f, 10.0f)
    };

    private List<EnemyPosition> Mintmoss_enemyPosition1 = new List<EnemyPosition>{
        new EnemyPosition(107.0f,0.7f),
        new EnemyPosition(128.0f,16.0f),
        new EnemyPosition(149.0f,1.3f),
        new EnemyPosition(223.0f,7.0f)
    };

    private List<EnemyPosition> Ichigogari_enemyPosition2 = new List<EnemyPosition>{
        new EnemyPosition(1.0f,50.0f),
        new EnemyPosition(221.0f,35.0f),
        new EnemyPosition(172.0f,64.0f),
        new EnemyPosition(57.0f,51.0f),
        new EnemyPosition(200.0f,68.0f),
        new EnemyPosition(75.0f,51.0f),
        new EnemyPosition(52.0f,31.0f)
    };

    private List<EnemyPosition> Mintmoss_enemyPosition2 = new List<EnemyPosition>{
        new EnemyPosition(217.0f,43.0f),
        new EnemyPosition(80.0f,56.0f),
        new EnemyPosition(158.0f,67.0f),
        new EnemyPosition(5.7f,52.0f),
        new EnemyPosition(171.0f,32.0f),
        new EnemyPosition(4.6f,32.0f),
        new EnemyPosition(95.0f,33.0f),
        new EnemyPosition(-47.0f,33.0f)
    };

    // Start is called before the first frame update
    void Start()
    {
        CreateEnemy1();
    }

    public void CreateEnemy1()
    {
        // 1F目
        foreach (var pos in Ichigogari_enemyPosition1)
        {
            Instantiate(itigogariPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
        foreach (var pos in Mintmoss_enemyPosition1)
        {
            Instantiate(mintmossPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
    }

    public void CreateEnemy2()
    {
        // 1F目
        foreach (var pos in Ichigogari_enemyPosition2)
        {
            Instantiate(itigogariPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
        foreach (var pos in Mintmoss_enemyPosition2)
        {
            Instantiate(mintmossPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
    }

    public void DestroyEnemy()
    {
        // 敵の削除
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Header("Target Settings")]
    public GameObject player; // プレイヤーのTransform

    [Header("Offset Settings")]
    public Vector3 offset = new Vector3(0, 2, -10); // カメラの位置オフセット

    //カメラがy軸方向に追従し始める上限下限
    //[SerializeField] float upperlimit;
    //[SerializeField] float lowerlimit;


    [Header("Smooth Settings")]
    public float smoothSpeed = 0.125f; // カメラの追従速度

    Vector3 CameraPosition = new Vector3(0.0f, 0.0f, -10.0f);
    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is not assigned to the CameraFollow script.");
            return;
        }

        // 目標位置の計算
        CameraPosition.x = player.transform.position.x + offset.x;

        /*if (player.transform.position.y >= upperlimit || player.transform.position.y <= lowerlimit)
        {
            CameraPosition.y = player.transform.position.y + offset.y;
        }
        else
        {
            CameraPosition.y = offset.y;
        }*/

        CameraPosition.y = player.transform.position.y + offset.y;

        // 緩やかな追従を実現するためにLerpを使用
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, CameraPosition, smoothSpeed);

        // カメラの位置を更新
        transform.position = smoothedPosition;
    }

}
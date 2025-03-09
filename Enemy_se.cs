using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_se : MonoBehaviour
{
    // 再生したいSE用のAudioClipをインスペクタから設定
    public AudioSource audiosource;
    public AudioClip destroyse;
    public AudioClip damagedse;

    public AudioClip houkouse;
    public AudioClip warpse;
    public AudioClip destroybossse;
    // audiosource コンポーネント

    void Start()
    {
        // audiosourceコンポーネントを取得
        audiosource = GetComponent<AudioSource>();

        // audiosourceがない場合はエラーメッセージを表示
        if (audiosource == null)
        {
            Debug.LogError("audiosource component is missing!");
        }
    }

    public void damaged()
    {
        // audiosourceが存在する場合のみ音を再生
        if (audiosource != null)
        {
            audiosource.PlayOneShot(damagedse); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
        }
    }
    void destroyme()
    {
        // audiosourceが存在する場合のみ音を再生
        if (audiosource != null)
        {
            audiosource.PlayOneShot(destroyse); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
        }
    }

    public void houkou()
    {
        if (audiosource != null)
        {
            audiosource.PlayOneShot(houkouse); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
        }
    }

    public void warp()
    {
        // audiosourceが存在する場合のみ音を再生
        if (audiosource != null)
        {
            audiosource.PlayOneShot(warpse); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
        }
    }

    public void destroy_boss()
    {
        // audiosourceが存在する場合のみ音を再生
        if (audiosource != null)
        {
            audiosource.PlayOneShot(destroybossse); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
        }
    }
}
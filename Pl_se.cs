using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_se : MonoBehaviour
{
    // 再生したいSE用のAudioClipをインスペクタから設定
    public AudioClip swing;
    public AudioClip jump;
    public AudioClip damagedse;
    public AudioClip fly_up;
    public AudioClip cannot_fly;
    private AudioSource audiosource;
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
    void atack()
    {
        // audiosourceが存在する場合のみ音を再生
        if (audiosource != null)
        {
            audiosource.PlayOneShot(swing); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
        }
    }
    public void jumping()
    {
        // audiosourceが存在する場合のみ音を再生
        if (audiosource != null)
        {
            audiosource.PlayOneShot(jump); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
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
    public void fly()
    {
        // audiosourceが存在する場合のみ音を再生
        if (audiosource != null)
        {
            audiosource.PlayOneShot(fly_up); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
        }
    }
    public void cantfly()
    {
        // audiosourceが存在する場合のみ音を再生
        if (audiosource != null)
        {
            audiosource.PlayOneShot(cannot_fly); // 一度きりの再生
        }
        else
        {
            Debug.LogError("audiosource or AudioClip is not set.");
        }
    }
}

using UnityEngine;

public class BGMController : MonoBehaviour
{
    private static BGMController instance;
    public AudioClip alice; //ステージBGM
    public AudioClip boss; //ボス戦 BGM
    public AudioClip ending; //エンディング
    private AudioSource audioSource;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        // AudioSource コンポーネントを取得または追加
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        PlayBGM(alice); // BGM 再生

    }

    public void PlayBGM(AudioClip audioClip)
    {
        // 現在のBGMを停止
        StopBGM();

        // 新しいBGMを設定して再生
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true; // ループ再生を有効化
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No BGM clip assigned!");
        }
    }

    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void ResumeBGM()
    {
        if (!audioSource.isPlaying && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public void PlayBossBgm()
    {
        PlayBGM(boss);
    }

    public void PlayEndingBgm()
    {
        PlayBGM(ending);
    }
}

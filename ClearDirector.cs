using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearDirector : MonoBehaviour
{
    private GameObject bgm;
    public AudioSource audiosource;
    public AudioClip select_se; //決定時のse
    private BGMController bgm_controller;
    void Start()
    {
        bgm = GameObject.Find("bgm");
        if (bgm != null)
        {
            bgm_controller = bgm.GetComponent<BGMController>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("z");
            bgm_controller.StopBGM();
            audiosource.PlayOneShot(select_se);
            SceneManager.LoadScene("Title");
        }
    }
}

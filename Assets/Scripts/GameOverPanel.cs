using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public GameObject FailImg;
    public GameObject FailBtn;
    public GameObject WinImg;
    public GameObject WinBtn;

    public AudioSource MySource;
    public AudioClip FailClip;
    public AudioClip WinClip;
    public bool IsInit = false;

    private void Start()
    {
        Init();
    }
    private void OnDisable()
    {
        if (MySource != null)
        {
            MySource.Stop();
        }
    }
    public void Init()
    {
        if (IsInit)
        {
            return;
        }
        IsInit = true;
        if (FailImg == null)
        {
            FailImg = transform.Find("FailImg").gameObject;
        }
        if (FailBtn == null)
        {
            FailBtn = transform.Find("FailBtn").gameObject;
        }
        if (WinImg == null)
        {
            WinImg = transform.Find("WinImg").gameObject;
        }
        if (WinBtn == null)
        {
            WinBtn = transform.Find("WinBtn").gameObject;
        }
        if (MySource == null)
        {
            MySource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        }
    }

    public void SetActive(bool active, bool isWin = false)
    {
        gameObject.SetActive(active);
        FailImg.SetActive(!isWin);
        FailBtn.SetActive(!isWin);
        WinBtn.SetActive(isWin);
        WinImg.SetActive(isWin);
        AudioClip clip = isWin ? WinClip : FailClip;
        MySource.PlayOneShot(clip);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePanel : MonoBehaviour
{
    public Image TaskImg;
    public Image BodyTitle;
    public Button CloseBtn;

    public bool IsInit;
    public void Start()
    {
        Init();
    }

    public void Init()
    {
        if (IsInit)
        {
            return;
        }
        IsInit = true;
        if (TaskImg == null)
        {
            TaskImg = transform.Find("TaskImg").GetComponent<Image>();
        }
        if (BodyTitle == null)
        {
            BodyTitle = transform.Find("BodyTitle").GetComponent<Image>();
        }
        if (CloseBtn == null)
        {
            CloseBtn = transform.Find("CloseBtn").GetComponent<Button>();
        }
        if (CloseBtn != null)
        {
            CloseBtn.onClick.AddListener(() =>
            {
                SetActive(false);
            });
        }
    }

    public void SetActive(bool active,Sprite bodyTitle = null,Sprite task = null)
    {
        gameObject.SetActive(active);
        if (bodyTitle != null &&  BodyTitle != null)
        {
            BodyTitle.sprite = bodyTitle;
        }
        if (task != null && TaskImg != null)
        {
            TaskImg.sprite = task;
        }
    }
}

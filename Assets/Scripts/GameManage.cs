using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public FaceInfo CurInfo = new FaceInfo(Emotion.Default, Emotion.Default, Emotion.Default, Emotion.Default);

    public Emotion TargetEmo;

    public EmotionInfo[] TasksSprites;
    public EmotionInfo[] EyeBrowSprites;
    public EmotionInfo[] EyeSprites;
    public EmotionInfo[] MouthSprites;

    [SerializeField]
    private MainPanel mainPanel;
    [SerializeField]
    private NoticePanel noticePanel;
    public void Awake()
    {
        if (mainPanel == null)
        {
            mainPanel = GetComponentInChildren<MainPanel>(true);
        }
        if (noticePanel == null)
        {
            noticePanel = GetComponentInChildren<NoticePanel>(true);
        }
        if (mainPanel != null)
        {
            mainPanel.Init();
            mainPanel.OnPartImgClick.AddListener(OnPartImgClick);
        }
        if (noticePanel != null)
        {
            noticePanel.Init();
        }
        CurInfo.IsEnd = onEnd;
        randomTatget();
        noticePanel.SetActive(true, null, getTask(TargetEmo));
    }

    public void ChanagePart(int part)
    {
        if (mainPanel != null)
        {
            switch ((Part)part)
            {
                default:
                case Part.EyeBrow:
                    mainPanel.SwitchPart(Part.EyeBrow, EyeBrowSprites);
                    break;
                case Part.Eye:
                    mainPanel.SwitchPart(Part.Eye, EyeSprites);
                    break;
                case Part.Mouth:
                    mainPanel.SwitchPart(Part.Mouth, MouthSprites);
                    break;
            }

        }
    }

    public void OnPartImgClick(Part part,Emotion emotion)
    {
        switch (part)
        {
            case Part.EyeBrow:
                CurInfo.EyeBrow = emotion;
                break;
            case Part.Eye:
                CurInfo.Eye = emotion;
                break;
            case Part.Mouth:
                CurInfo.Mouth = emotion;
                break;
        }
    }

    public void OnNoticeClose()
    {
        mainPanel.SetActive(true);
    }
    private void onEnd()
    {
        randomTatget();
        mainPanel.SetActive(false);
        noticePanel.SetActive(true);
    }

    private void randomTatget()
    {
        int random = UnityEngine.Random.Range(1, 6);
        TargetEmo = (Emotion)random;
        CurInfo.Restart(TargetEmo);
        mainPanel.SwitchPart(Part.EyeBrow, EyeBrowSprites);
    }

    private Sprite getTask(Emotion emotion)
    {
        if (TasksSprites != null)
        {
            foreach (var item in TasksSprites)
            {
                if (item.Type == emotion)
                {
                    return item.TargetSprite;
                }
            }
        }
        return null;
    }
}

[System.Serializable]
public class FaceInfo
{
    public Emotion EyeBrow
    {
        get
        {
            return m_eyeBrow;
        }
        set
        {
            m_eyeBrow = value;
            m_eyeBrowChanged = true;
            if (isChanged())
            {
                IsEnd?.Invoke();
            }
        }
    }
    public Emotion Eye
    {
        get
        {
            return m_eye;
        }
        set
        {
            m_eye = value;
            m_eyeChanged = true;
            if (isChanged())
            {
                IsEnd?.Invoke();
            }
        }
    }
    public Emotion Mouth
    {
        get
        {
            return m_mouth;
        }
        set
        {
            m_mouth = value;
            m_mouthChanged = true;
            if (isChanged())
            {
                IsEnd?.Invoke();
            }
        }
    }
    public Action IsEnd;

    [SerializeField]
    private Emotion m_eyeBrow;
    [SerializeField]
    private Emotion m_eye;
    [SerializeField]
    private Emotion m_mouth;

    [SerializeField]
    private Emotion m_target;
    private bool m_eyeBrowChanged;
    private bool m_eyeChanged;
    private bool m_mouthChanged;
    public FaceInfo(Emotion eyeBrow, Emotion eye, Emotion mouth, Emotion target)
    {
        m_eyeBrow = eyeBrow;
        m_eye = eye;
        m_mouth = mouth;
        m_target = target;
    }

    public void Restart(Emotion target)
    {
        m_target = target;
        m_eyeBrowChanged = false;
        m_eyeChanged = false;
        m_mouthChanged = false;
    }

    public bool IsRight()
    {
        return m_eyeBrow == m_target && m_eye == m_target && m_mouth == m_target;
    }
    private bool isChanged()
    {
        return m_eyeBrowChanged && m_eyeChanged && m_mouthChanged;
    }
}
[System.Serializable]
public struct EmotionInfo
{
    public Emotion Type;
    public Sprite TargetSprite;
}

[System.Serializable]
public enum Part
{
    EyeBrow,
    Eye,
    Mouth,
}

[System.Serializable]
public enum Emotion
{
    Default,
    Anger,
    Cry,
    Happy,
    Scare,
    Tired
}
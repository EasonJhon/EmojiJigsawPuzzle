using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainPanel : MonoBehaviour
{
    public Image EyeBrowImg;
    public Image EyeImg;
    public Image MouthImg;

    public Image TimeImg;
    public Text TimeTxt;
    public float TimeOut = 60f;

    public PartEvent OnPartImgClick = new PartEvent();
    public UnityEvent OnTimeOut = new UnityEvent();
    public class PartEvent : UnityEvent<Part, Emotion> { }

    public Part CurPart = Part.EyeBrow;
    public Dictionary<Emotion, Button> PartImgs = new Dictionary<Emotion, Button>();

    public bool IsInit;
    public bool IsStart;
    private float _timeOut;
    public void Start()
    {
        Init();
    }

    private void Update()
    {
        if (IsStart)
        {
            _timeOut -= Time.deltaTime;
            if (TimeTxt != null)
            {
                TimeTxt.text = _timeOut.ToString("F0") + "��";
            }
            if (_timeOut <= 0)
            {
                IsStart = false;
                OnTimeOut?.Invoke();
            }
        }
    }

    public void Init()
    {
        if (IsInit)
        {
            return;
        }
        IsInit = true;
        if (EyeBrowImg == null)
        {
            EyeBrowImg = transform.Find("EyeBrowImg").GetComponent<Image>();
        }
        if (EyeImg == null)
        {
            EyeImg = transform.Find("EyesImg").GetComponent<Image>();
        }
        if (MouthImg == null)
        {
            MouthImg = transform.Find("MouthImg").GetComponent<Image>();
        }
        string partRoot = "BottomSprites/{0}";
        foreach (var item in System.Enum.GetNames(typeof(Emotion)))
        {
            string path = string.Format(partRoot, item);
            Transform trans = transform.Find(path);
            if (trans != null)
            {
                Button button = trans.GetComponent<Button>() ?? trans.gameObject.AddComponent<Button>();
                Emotion emotion = (Emotion)System.Enum.Parse(typeof(Emotion), item);
                PartImgs.Add(emotion, button);
                button.onClick.AddListener(() => { OnPartImgsClick(emotion); });
            }
        }
    }

    public void SetActive(bool active)
    {
        IsStart = active;
        gameObject.SetActive(active);
        TimeImg.gameObject.SetActive(!active);
        TimeTxt.gameObject.SetActive(active);
        _timeOut = TimeOut;
    }
    public void SetEyeBrow(Sprite sprite)
    {
        if (EyeBrowImg != null && sprite != null)
        {
            EyeBrowImg.sprite = sprite;
            EyeBrowImg.SetImgBySpriteSize(1.000001f / 3.000001f);
        }
    }
    public void SetEye(Sprite sprite)
    {
        if (EyeImg != null && sprite != null)
        {
            EyeImg.sprite = sprite;
            EyeImg.SetImgBySpriteSize(1.000001f / 3.000001f);
        }
    }
    public void SetMouth(Sprite sprite)
    {
        if (MouthImg != null && sprite != null)
        {
            MouthImg.sprite = sprite;
            MouthImg.SetImgBySpriteSize(1.000001f / 3.000001f);
        }
    }

    public void OnPartImgsClick(Emotion emotion)
    {
        Debug.Log(" OnPartImgsClick : " + emotion.ToString());
        if (PartImgs.ContainsKey(emotion))
        {
            Image image = PartImgs[emotion].transform.GetChild(0).GetComponent<Image>();
            switch (CurPart)
            {
                default:
                case Part.EyeBrow:
                    SetEyeBrow(image.sprite);
                    break;
                case Part.Eye:
                    SetEye(image.sprite);
                    break;
                case Part.Mouth:
                    SetMouth(image.sprite);
                    break;
            }
        }
        OnPartImgClick?.Invoke(CurPart,emotion);
    }
    public void SwitchPart(Part part, EmotionInfo[] emotions)
    {
        if (emotions != null)
        {
            int random = Random.Range(1, 6);
            Vector2 start = PartImgs[(Emotion)random].GetComponent<RectTransform>().anchoredPosition;
            for (int i = 0; i < emotions.Length; i++)
            {
                if (PartImgs.ContainsKey(emotions[i].Type))
                {
                    RectTransform rect = PartImgs[emotions[i].Type].GetComponent<RectTransform>();
                    rect.anchoredPosition = start;
                    start.x += 200;
                    if (start.x > 400)
                    {
                        start.x = -400f;
                    }
                    Image image = PartImgs[emotions[i].Type].transform.GetChild(0).GetComponent<Image>();
                    image.sprite = emotions[i].TargetSprite;
                    image.SetImgBySpriteSize(0.25f);

                }
            }
        }
        CurPart = part;
    }

    public Image GetEmotionInfoImg(Emotion emotion)
    {
 
        return null;
    }
}
 

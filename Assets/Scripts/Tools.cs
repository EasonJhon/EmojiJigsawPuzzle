using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Tools 
{

    public static void SetImgBySpriteSize(this Image image,float multiples)
    {
        if (image.sprite != null)
        {
            Vector2 size = image.sprite.rect.size;
            Debug.Log(" Target Img Sprite Size : " + size);
            size *= multiples;
            image.rectTransform.sizeDelta = size;
        }
    }
}

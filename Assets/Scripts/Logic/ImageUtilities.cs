using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageUtilities : MonoBehaviour
{
    public static ImageUtilities instance { get; private set; }

    public Sprite woodSprite;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Sprite GetSpriteForResource(Resources resource)
    {
        switch(resource)
        {
            case Resources.WOOD:
                {
                    return woodSprite;
                }
        }
        return null;
    }
}

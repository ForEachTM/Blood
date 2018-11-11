using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelFonts : MonoBehaviour
{
    Font[] Fonts;

    void Start()
    {
        Fonts = Resources.LoadAll<Font>("Fonts");

        foreach(Font Font in Fonts)
        {
            Font.material.mainTexture.filterMode = FilterMode.Point;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextProperties : MonoBehaviour
{
    Text[] texts;

    Shadow[] shadows;

    public Color textColor, shadowColor;

    // Start is called before the first frame update
    void Start()
    {
        texts = FindObjectsOfType<Text>();
        shadows = FindObjectsOfType<Shadow>();

        foreach (Text text in texts)
        {
            text.color = textColor;
        }

        foreach (Shadow shadow in shadows)
        {
            shadow.effectColor = shadowColor;
        }
    }
}

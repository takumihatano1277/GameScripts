using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIの色をscript上から大きく変更
/// </summary>
public class LogoHueScript : MonoBehaviour
{
    private UIHue UI;
    private bool colorUP;
    private bool colorDown;
    private float time = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        UI=GetComponent<UIHue>();
        colorUP = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(colorUP)
        {
            UI.hue += time;
            if(UI.hue>=360.0f)
            {
                UI.hue = 360;
                colorUP = false;
                colorDown = true;
            }
        }
        else if (colorDown)
        {
            UI.hue -= time;
            if (UI.hue <= 0.0f)
            {
                UI.hue = 0;
                colorUP = true;
                colorDown = false;
            }
        }
    }
}

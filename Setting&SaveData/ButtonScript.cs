using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    void Start()
    {
        FadeManager.FadeIn();
    }
    /// <summary>
    /// どのシーンに移動するか
    /// </summary>
    /// <param name="scene">移動先のシーン番号</param>
    public void ReStartButton(int scene)
    {
        FadeManager.FadeOut(scene);
    }
}

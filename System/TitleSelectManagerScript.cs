using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class TitleSelectManagerScript : MonoBehaviour
{
    private void Awake()
    {
        FadeManager.FadeIn();
        TitleBGMOn();
    }
    /// <summary>
    /// タイトルBGMに変更
    /// </summary>
    public void TitleBGMOn()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.TITLE_BGM, 2, 1, 1, 0, 1, true);
    }
}

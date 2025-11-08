using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class ResultManagerScript : MonoBehaviour
{
    [SerializeField] private bool clear_or_Over;    //クリアシーンかどうか
    private void Awake()
    {
        FadeManager.FadeIn();
        if (clear_or_Over)
        {
            GameClearBGMOn();
        }
        else
        {
            GameOverBGMOn();
        }
    }
    /// <summary>
    /// ゲームオーバーBGMに変更
    /// </summary>
    public void GameOverBGMOn()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.GAMEOVER3, 0.5f, 1, 1, 0, 1, false, () =>
        {
            BGMSwitcher.FadeOutAndFadeIn(BGMPath.GAME_OVER2, 2, 1, 1, 0, 1, true);
        });
    }
    /// <summary>
    /// ゲームクリアBGMに変更
    /// </summary>
    public void GameClearBGMOn()
    {
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.FANTASY14, 2, 1, 1, 0, 1, true);
    }
}

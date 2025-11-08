using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
using UnityEngine.SceneManagement;
using Rewired;

public class GameManagerScript : MonoBehaviour
{
    static public bool blueCheck, greenCheck, redCheck;     //松明に火が灯っているかどうか
    static public int stageNo;          //現在のステージ番号

    private int tutorialAllScene = 4;     //buildindexからチュートリアルオールシーンが何番か

    private static bool isWallStop = false;         //プレイヤーに迫るトラップ
    private static bool isCameraStop = false;       //カメラの動きを止める状態
    private static bool isHalfSave = false;         //ステージのセーブポイントに火が灯っているかどうか
    private static bool isPaused = false;           //ポーズしているかどうか
    private Rewired.Player player0;

    private void Awake()
    {
        Init();
    }
    void Init()
    {
        player0 = ReInput.players.GetPlayer(0);
        isWallStop = false;       //迫ってくる壁
        isCameraStop = false;     //カメラ停止を無効化
        FadeManager.FadeIn();
        BGMSwitcher.FadeOutAndFadeIn(BGMPath.BGM01);        //ゲームBGM
        stageNo = SceneManager.GetActiveScene().buildIndex - tutorialAllScene;
        if (stageNo >= tutorialAllScene)   //チュートリアルALLをクリアした時とチュートリアル全てをクリアした時が同じ進行度になるように調整
            stageNo--;

        LoadingScript.reStartNo = SceneManager.GetActiveScene().buildIndex;
        blueCheck = greenCheck = redCheck = false;      //ステージの松明の進行度を初期化
        Resume();
    }
    /// <summary>
    /// ポーズ中
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
    }
    /// <summary>
    /// ポーズ解除
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
    /// <summary>
    /// 中間地点のセーブ処理
    /// </summary>
    /// <param name="_save">セーブさせるか</param>
    public static void SavePoint(bool _save)
    {
        isHalfSave = _save;
    }
    /// <summary>
    /// カメラ停止処理
    /// </summary>
    /// <param name="_cameraStop">カメラ停止させるか</param>
    public static void CameraStop(bool _cameraStop)
    {
        isCameraStop = _cameraStop;
    }
    /// <summary>
    /// 壁停止処理
    /// </summary>
    /// <param name="_wallStop">停止させるか</param>
    public static void WallStop(bool _wallStop)
    {
        isWallStop = _wallStop;
    }
    /// <summary>
    /// ポーズ中かどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsPaused()
    {
        return isPaused;
    }
    /// <summary>
    /// 中間地点に火が灯されているかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsHalfSave()
    {
        return isHalfSave;
    }
    /// <summary>
    /// カメラ停止処理がされているかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsCameraStop()
    {
        return isCameraStop;
    }
    /// <summary>
    /// 壁停止処理がされているかどうか
    /// </summary>
    /// <returns></returns>
    public static bool IsWallStop()
    {
        return isWallStop;
    }
}

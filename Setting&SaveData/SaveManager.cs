using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public struct SaveData
{
    public bool HUD;
    public bool tutorial;
    public bool highlight;

    public int clearStage;
    public bool[] stageData;

    public string leftWalk1, leftWalk2, rightWalk1, rightWalk2, jump1, jump2, jump3, crouching1, crouching2, crouching3,
        shot1, shot2, shot3, fire1, fire2, fire3, modeChange1, modeChange2, modeChange3;

    public bool[] stageStar;

    public float master, bgm, se;
}

public static class SaveManager
{
    public static MyBase64str base64 = new MyBase64str("UTF-8");
    public static SaveData sd;
    const string SAVE_FILE_PATH = "save.json";
    
    /// <summary>
    /// データを保存
    /// </summary>
    public static void save()
    {
        string json = JsonUtility.ToJson(sd);
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory();
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
        path += ("/" + SAVE_FILE_PATH);
        StreamWriter writer = new StreamWriter(path, false);

        writer.WriteLine(base64.Encode(json));
        writer.Flush();
        writer.Close();
    }
    /// <summary>
    /// 保存しているデータを読み込み
    /// </summary>
    public static void load()
    {
        try
        {
#if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory();
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
            FileInfo info = new FileInfo(path + "/" + SAVE_FILE_PATH);
            StreamReader reader = new StreamReader(info.OpenRead());
            string json = reader.ReadToEnd();
            sd = JsonUtility.FromJson<SaveData>(base64.Decode(json));
        }
        catch (Exception e)
        {
            sd = new SaveData();
        }
    }

    #region セーブ
    /// <summary>
    /// マスターボリューム保存
    /// </summary>
    /// <param name="_master">マスターの音量</param>
    public static void SaveMaster(float _master)
    {
        sd.master = _master;
        save();
    }
    /// <summary>
    /// SEボリューム保存
    /// </summary>
    /// <param name="_se">SEの音量</param>
    public static void SaveSE(float _se)
    {
        sd.se = _se;
        save();
    }
    /// <summary>
    /// BGMボリューム保存
    /// </summary>
    /// <param name="_bgm">BGMの音量</param>
    public static void SaveBGM(float _bgm)
    {
        sd.bgm = _bgm;
        save();
    }
    /// <summary>
    /// HUD表示するかどうか
    /// </summary>
    /// <param name="_HUD">オンかオフか</param>
    public static void SaveHUD(bool _HUD)
    {
        sd.HUD = _HUD;
        save();
    }
    /// <summary>
    /// チュートリアルウィンドウを表示するかどうか
    /// </summary>
    /// <param name="_tutorial">オンかオフか</param>
    public static void SaveTutorial(bool _tutorial)
    {
        sd.tutorial = _tutorial;
        save();
    }
    /// <summary>
    /// ハイライトをオンにするかどうか
    /// </summary>
    /// <param name="_highlight">オンかオフか</param>
    public static void SaveHighlight(bool _highlight)
    {
        sd.highlight = _highlight;
        save();
    }
    /// <summary>
    /// ステージのデータを入れる場所を作る
    /// </summary>
    /// <param name="_stageNo">ステージが何個あるか</param>
    public static void saveStageGenerate(int _stageNo)
    {
        sd.stageData = new bool[_stageNo * 5 + 1];
    }
    /// <summary>
    /// 現在のステージの進行度を保存
    /// </summary>
    /// <param name="_blue">青い松明が灯されているか</param>
    /// <param name="_green">緑の松明が灯されているか</param>
    /// <param name="_red">赤い松明が灯されているか</param>
    /// <param name="_sceneNo">現在のステージの番号は何番か</param>
    public static void saveStage(bool _blue, bool _green, bool _red, int _sceneNo)
    {
        if (!sd.stageData[_sceneNo * 3 - 2])
        {
            sd.stageData[_sceneNo * 3 - 2] = _blue;
        }
        if (!sd.stageData[_sceneNo * 3 - 1])
        {
            sd.stageData[_sceneNo * 3 - 1] = _green;
        }
        if (!sd.stageData[_sceneNo * 3])
        {
            sd.stageData[_sceneNo * 3] = _red;
        }
        save();
    }
    /// <summary>
    /// 隠し要素のデータを入れる場所を作る
    /// </summary>
    public static void saveStageStar()
    {
        sd.stageStar = new bool[6];
    }
    /// <summary>
    /// 隠し要素の進行度を保存
    /// </summary>
    /// <param name="_stage">現在のステージの番号</param>
    /// <param name="_hidden">隠し要素を灯したか</param>
    public static void saveStageHidden(int _stage, bool _hidden)
    {
        if(!sd.stageStar[_stage])
        {
            sd.stageStar[_stage] = _hidden;
            save();
        }
    }
    /// <summary>
    /// 隠し要素データ初期化
    /// </summary>
    public static void saveStageHiddenReset()
    {
        for (int i = 0; i < sd.stageStar.Length; i++)
        {
            sd.stageStar[i] = false;
        }
        save();
    }
    /// <summary>
    /// 現在のクリアステージはどこまで行ったか
    /// </summary>
    /// <param name="_clearStage">クリアした最後のステージの番号</param>
    public static void saveClearStage(int _clearStage)
    {
        if(sd.clearStage<_clearStage)
        {
            sd.clearStage = _clearStage;
            save();
        }
    }
    
    /// <summary>
    /// 左移動キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveLeftWalk1(string _keyName)
    {
        sd.leftWalk1 = _keyName;
        save();
    }
    /// <summary>
    /// 左移動キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveLeftWalk2(string _keyName)
    {
        sd.leftWalk2 = _keyName;
        save();
    }
    /// <summary>
    /// 右移動キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveRightWalk1(string _keyName)
    {
        sd.rightWalk1 = _keyName;
        save();
    }
    /// <summary>
    /// 右移動キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveRightWalk2(string _keyName)
    {
        sd.rightWalk2 = _keyName;
        save();
    }
    /// <summary>
    /// ジャンプキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveJump1(string _keyName)
    {
        sd.jump1 = _keyName;
        save();
    }
    /// <summary>
    /// ジャンプキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveJump2(string _keyName)
    {
        sd.jump2 = _keyName;
        save();
    }
    /// <summary>
    /// ジャンプキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveJump3(string _keyName)
    {
        sd.jump3 = _keyName;
        save();
    }
    /// <summary>
    /// 蝋発射キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveShot1(string _keyName)
    {
        sd.shot1 = _keyName;
        save();
    }
    /// <summary>
    /// 蝋発射キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveShot2(string _keyName)
    {
        sd.shot2 = _keyName;
        save();
    }
    /// <summary>
    /// 蝋発射キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveShot3(string _keyName)
    {
        sd.shot3 = _keyName;
        save();
    }
    /// <summary>
    /// 着火キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveFire1(string _keyName)
    {
        sd.fire1 = _keyName;
        save();
    }
    /// <summary>
    /// 着火キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveFire2(string _keyName)
    {
        sd.fire2 = _keyName;
        save();
    }
    /// <summary>
    /// 着火キー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveFire3(string _keyName)
    {
        sd.fire3 = _keyName;
        save();
    }
    /// <summary>
    /// しゃがみキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveCrouching1(string _keyName)
    {
        sd.crouching1 = _keyName;
        save();
    }
    /// <summary>
    /// しゃがみキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveCrouching2(string _keyName)
    {
        sd.crouching2 = _keyName;
        save();
    }
    /// <summary>
    /// しゃがみキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveCrouching3(string _keyName)
    {
        sd.crouching3 = _keyName;
        save();
    }
    /// <summary>
    /// 点火切り替えキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveModeChange1(string _keyName)
    {
        sd.modeChange1 = _keyName;
        save();
    }
    /// <summary>
    /// 点火切り替えキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveModeChange2(string _keyName)
    {
        sd.modeChange2 = _keyName;
        save();
    }
    /// <summary>
    /// 点火切り替えキー・ボタン保存
    /// </summary>
    /// <param name="_keyName">保存するキー</param>
    public static void SaveModeChange3(string _keyName)
    {
        sd.modeChange3 = _keyName;
        save();
    }
    #endregion
}
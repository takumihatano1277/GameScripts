using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;
using UnityEngine.UI;
using System.IO;

public class ConfigScript : MonoBehaviour
{
    private static ConfigScript instance = null;
    private const string PATHFILE = "データ初期化";       //データが初期化されたか確認
    private const string SAVEFILE = "save.json";          //データを格納
    static public bool highlight;                         //ハイライトをオンにするかどうか


    Selectable<bool> mSelectedValue = new Selectable<bool>();
    private void Awake()
    {
        mSelectedValue.mChanged += value => highlight = value;
    }
    /// <summary>
    /// ハイライト機能をオンにする
    /// </summary>
    public void HighlightOn()
    {
        mSelectedValue.SetValueIfNotEqual(true);
    }
    /// <summary>
    /// ハイライト機能をオフにする
    /// </summary>
    public void HighlightOff()
    {
        mSelectedValue.SetValueIfNotEqual(false);
    }
    /// <summary>
    /// データ削除関数
    /// </summary>
    public void DataDelete()
    {
        string path = Directory.GetCurrentDirectory();
        string savefile = Directory.GetCurrentDirectory();

        if (File.Exists(path += ("/" + SAVEFILE)))
        {
            File.Delete(path);
            File.Delete(savefile += ("/" + PATHFILE));
            Debug.Log("データ削除完了");
            return;
        }
    }
    /// <summary>
    /// データ削除後に確認を押すとロゴSceneへ移動
    /// </summary>
    public void DeleteDone()
    {
        FadeManager.FadeOut(0);
    }
}

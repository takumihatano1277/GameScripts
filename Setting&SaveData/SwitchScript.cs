using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Frost;
using UnityEngine.Events;

public class SwitchScript : MonoBehaviour
{
    SwitchManager switchManager;
    [SerializeField] private UnityEvent SwitchUpdate;
    static public bool HUDOff;
    private string name;
    private void Awake()
    {
        switchManager = GetComponent<SwitchManager>();
        SwitchUpdate.Invoke();
    }
    /// <summary>
    /// HUDを表示するかどうかを保存
    /// </summary>
    /// <param name="_save">表示するかどうか</param>
    public void SaveHUD(bool _save)
    {
        SaveManager.SaveHUD(_save);
    }
    /// <summary>
    /// ハイライトを表示するかどうかを保存
    /// </summary>
    /// <param name="_save">表示するかどうか</param>
    public void SaveHighlight(bool _save)
    {
        SaveManager.SaveHighlight(_save);
    }
    /// <summary>
    /// チュートリアルウィンドウを表示するかどうかを保存
    /// </summary>
    /// <param name="_save">表示するかどうか</param>
    public void SaveTutorial(bool _save)
    {
        SaveManager.SaveTutorial(_save);
    }
    /// <summary>
    /// HUDを表示するかどうかを読み込み
    /// </summary>
    public void SwitchHUD()
    {
        switchManager.isOn = DataGetScript.getHUD();
    }
    /// <summary>
    /// ハイライトを表示するかどうかを読み込み
    /// </summary>
    public void SwitchHighlight()
    {
        switchManager.isOn = DataGetScript.getHighlight();
    }
    /// <summary>
    /// チュートリアルウィンドウを表示するかどうかを読み込み
    /// </summary>
    public void SwitchTutorial()
    {
        switchManager.isOn = DataGetScript.getTutorial();
    }
}

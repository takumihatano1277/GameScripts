using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataGetScript : MonoBehaviour
{
    /// <summary>
    /// HUDを表示するかどうか
    /// </summary>
    /// <returns></returns>
    static public bool getHUD()
    {
        return SaveManager.sd.HUD;
    }
    /// <summary>
    /// ハイライトを表示するかどうか
    /// </summary>
    /// <returns></returns>
    static public bool getHighlight()
    {
        return SaveManager.sd.highlight;
    }
    /// <summary>
    /// チュートリアルを表示するかどうか
    /// </summary>
    /// <returns></returns>
    static public bool getTutorial()
    {
        return SaveManager.sd.tutorial;
    }
    /// <summary>
    /// 保存されている左移動キー1
    /// </summary>
    /// <returns></returns>
    static public string getLeftWalk1()
    {
        return SaveManager.sd.leftWalk1;
    }
    /// <summary>
    /// 保存されている左移動キー2
    /// </summary>
    /// <returns></returns>
    static public string getLeftWalk2()
    {
        return SaveManager.sd.leftWalk2;
    }
    /// <summary>
    /// 保存されている右移動キー1
    /// </summary>
    /// <returns></returns>
    static public string getRightWalk1()
    {
        return SaveManager.sd.rightWalk1;
    }
    /// <summary>
    /// 保存されている右移動キー2
    /// </summary>
    /// <returns></returns>
    static public string getRightWalk2()
    {
        return SaveManager.sd.rightWalk2;
    }
    /// <summary>
    /// 保存されているジャンプキー1
    /// </summary>
    /// <returns></returns>
    static public string getJump1()
    {
        return SaveManager.sd.jump1;
    }
    /// <summary>
    /// 保存されているジャンプキー2
    /// </summary>
    /// <returns></returns>
    static public string getJump2()
    {
        return SaveManager.sd.jump2;
    }
    /// <summary>
    /// 保存されているジャンプボタン
    /// </summary>
    /// <returns></returns>
    static public string getJump3()
    {
        return SaveManager.sd.jump3;
    }
    /// <summary>
    /// 保存されているしゃがみキー1
    /// </summary>
    /// <returns></returns>
    static public string getCrouching1()
    {
        return SaveManager.sd.crouching1;
    }
    /// <summary>
    /// 保存されているしゃがみキー2
    /// </summary>
    /// <returns></returns>
    static public string getCrouching2()
    {
        return SaveManager.sd.crouching2;
    }
    /// <summary>
    /// 保存されているしゃがみボタン
    /// </summary>
    /// <returns></returns>
    static public string getCrouching3()
    {
        return SaveManager.sd.crouching3;
    }
    /// <summary>
    /// 保存されている着火キー1
    /// </summary>
    /// <returns></returns>
    static public string getFire1()
    {
        return SaveManager.sd.fire1;
    }
    /// <summary>
    /// 保存されている着火キー2
    /// </summary>
    /// <returns></returns>
    static public string getFire2()
    {
        return SaveManager.sd.fire2;
    }
    /// <summary>
    /// 保存されている着火ボタン
    /// </summary>
    /// <returns></returns>
    static public string getFire3()
    {
        return SaveManager.sd.fire3;
    }
    /// <summary>
    /// 保存されている蝋発射キー1
    /// </summary>
    /// <returns></returns>
    static public string getShot1()
    {
        return SaveManager.sd.shot1;
    }
    /// <summary>
    /// 保存されている蝋発射キー2
    /// </summary>
    /// <returns></returns>
    static public string getShot2()
    {
        return SaveManager.sd.shot2;
    }
    /// <summary>
    /// 保存されている蝋発射ボタン
    /// </summary>
    /// <returns></returns>
    static public string getShot3()
    {
        return SaveManager.sd.shot3;
    }
    /// <summary>
    /// 保存されている点火キー1
    /// </summary>
    /// <returns></returns>
    static public string getModeChange1()
    {
        return SaveManager.sd.modeChange1;
    }
    /// <summary>
    /// 保存されている点火キー2
    /// </summary>
    /// <returns></returns>
    static public string getModeChange2()
    {
        return SaveManager.sd.modeChange2;
    }
    /// <summary>
    /// 保存されている点火ボタン
    /// </summary>
    /// <returns></returns>
    static public string getModeChange3()
    {
        return SaveManager.sd.modeChange3;
    }
}

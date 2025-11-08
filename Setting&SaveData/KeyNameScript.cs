using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class KeyNameScript : MonoBehaviour
{
    private string keyBindName;
    private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI bindKeyName;
    [SerializeField] private UnityEvent KeyUpdate;
    static public int leftWalk, rightWalk, jump, crouching, fire, shot, modeChange;
    private bool set;
    static public bool controller;
    void Start()
    {
        keyText = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        keyBindName = transform.parent.gameObject.name;
        KeyUpdate.Invoke();
    }
    void Update()
    {
        if(set)
        {
            KeyUpdate.Invoke();
        }
    }
    /// <summary>
    /// 左移動キー変更
    /// </summary>
    /// <param name="num">キーボードの2つかコントローラーか</param>
    public void LeftWalkKey(int num)
    {
        leftWalk = num;
        if(num==3)
        {
            controller = true;
        }
        else
        {
            controller = false;
        }
    }
    /// <summary>
    /// 右移動キー変更
    /// </summary>
    /// <param name="num">キーボードの2つかコントローラーか</param>
    public void RightWalkKey(int num)
    {
        rightWalk = num;
        if (num == 3)
        {
            controller = true;
        }
        else
        {
            controller = false;
        }
    }
    /// <summary>
    /// ジャンプキー変更
    /// </summary>
    /// <param name="num">キーボードの2つかコントローラーか</param>
    public void JumpKey(int num)
    {
        jump = num;
        if (num == 3)
        {
            controller = true;
        }
        else
        {
            controller = false;
        }
    }
    /// <summary>
    /// しゃがみキー変更
    /// </summary>
    /// <param name="num">キーボードの2つかコントローラーか</param>
    public void CrouchingKey(int num)
    {
        crouching = num;
        if (num == 3)
        {
            controller = true;
        }
        else
        {
            controller = false;
        }
    }
    /// <summary>
    /// 着火キー変更
    /// </summary>
    /// <param name="num">キーボードの2つかコントローラーか</param>
    public void FireKey(int num)
    {
        fire = num;
        if (num == 3)
        {
            controller = true;
        }
        else
        {
            controller = false;
        }
    }
    /// <summary>
    /// 蝋発射キー変更
    /// </summary>
    /// <param name="num">キーボードの2つかコントローラーか</param>
    public void ShotKey(int num)
    {
        shot = num;
        if (num == 3)
        {
            controller = true;
        }
        else
        {
            controller = false;
        }
    }
    /// <summary>
    /// 点火切り替えキー変更
    /// </summary>
    /// <param name="num">キーボードの2つかコントローラーか</param>
    public void ModeChangeKey(int num)
    {
        modeChange = num;
        if (num == 3)
        {
            controller = true;
        }
        else
        {
            controller = false;
        }
    }
    /// <summary>
    /// キーの切り替えができるかどうか
    /// </summary>
    public void KeyNameAcquisition()
    {
        set = true;
        bindKeyName.text = "PRESS A KEY TO BIND <b>" + keyBindName + "</b>";
    }

    #region ロード
    /// <summary>
    /// 現在の左移動キー・ボタンを取得
    /// </summary>
    public void LoadLeftWalk1()
    {
        keyText.text = DataGetScript.getLeftWalk1();
    }
    /// <summary>
    /// 現在の左移動キー・ボタンを取得
    /// </summary>
    public void LoadLeftWalk2()
    {
        keyText.text = DataGetScript.getLeftWalk2();
    }
    /// <summary>
    /// 現在の右移動キー・ボタンを取得
    /// </summary>
    public void LoadRightWalk1()
    {
        keyText.text = DataGetScript.getRightWalk1();
    }
    /// <summary>
    /// 現在の右移動キー・ボタンを取得
    /// </summary>
    public void LoadRightWalk2()
    {
        keyText.text = DataGetScript.getRightWalk2();
    }
    /// <summary>
    /// 現在のジャンプキー・ボタンを取得
    /// </summary>
    public void LoadJump1()
    {
        keyText.text = DataGetScript.getJump1();
    }
    /// <summary>
    /// 現在のジャンプキー・ボタンを取得
    /// </summary>
    public void LoadJump2()
    {
        keyText.text = DataGetScript.getJump2();
    }
    /// <summary>
    /// 現在のジャンプキー・ボタンを取得
    /// </summary>
    public void LoadJump3()
    {
        keyText.text = DataGetScript.getJump3();
    }
    /// <summary>
    /// 現在のしゃがみキー・ボタンを取得
    /// </summary>
    public void LoadCrouching1()
    {
        keyText.text = DataGetScript.getCrouching1();
    }
    /// <summary>
    /// 現在のしゃがみキー・ボタンを取得
    /// </summary>
    public void LoadCrouching2()
    {
        keyText.text = DataGetScript.getCrouching2();
    }
    /// <summary>
    /// 現在のしゃがみキー・ボタンを取得
    /// </summary>
    public void LoadCrouching3()
    {
        keyText.text = DataGetScript.getCrouching3();
    }
    /// <summary>
    /// 現在の着火キー・ボタンを取得
    /// </summary>
    public void LoadFire1()
    {
        keyText.text = DataGetScript.getFire1();
    }
    /// <summary>
    /// 現在の着火キー・ボタンを取得
    /// </summary>
    public void LoadFire2()
    {
        keyText.text = DataGetScript.getFire2();
    }
    /// <summary>
    /// 現在の着火キー・ボタンを取得
    /// </summary>
    public void LoadFire3()
    {
        keyText.text = DataGetScript.getFire3();
    }
    /// <summary>
    /// 現在の蝋発射キー・ボタンを取得
    /// </summary>
    public void LoadShot1()
    {
        keyText.text = DataGetScript.getShot1();
    }
    /// <summary>
    /// 現在の蝋発射キー・ボタンを取得
    /// </summary>
    public void LoadShot2()
    {
        keyText.text = DataGetScript.getShot2();
    }
    /// <summary>
    /// 現在の蝋発射キー・ボタンを取得
    /// </summary>
    public void LoadShot3()
    {
        keyText.text = DataGetScript.getShot3();
    }
    /// <summary>
    /// 現在の点火切り替えキー・ボタンを取得
    /// </summary>
    public void LoadModeChange1()
    {
        keyText.text = DataGetScript.getModeChange1();
    }
    /// <summary>
    /// 現在の点火切り替えキー・ボタンを取得
    /// </summary>
    public void LoadModeChange2()
    {
        keyText.text = DataGetScript.getModeChange2();
    }
    /// <summary>
    /// 現在の点火切り替えキー・ボタンを取得
    /// </summary>
    public void LoadModeChange3()
    {
        keyText.text = DataGetScript.getModeChange3();
    }
    #endregion
}

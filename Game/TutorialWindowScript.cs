using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using KanKikuchi.AudioManager;
using Rewired;

public class TutorialWindowScript : MonoBehaviour
{
    [SerializeField] private UnityEvent WindowOn;
    [SerializeField] private UnityEvent WindowOff;
    private TextMeshProUGUI tutorialWindow;
    
    /// <summary>
    /// コントローラー検知
    /// </summary>
    private void Awake()
    {
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
        ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;
    }
    void Start()
    {
        tutorialWindow = transform.parent.gameObject.GetComponent<TextMeshProUGUI>();
    }
    void OnControllerConnected(ControllerStatusChangedEventArgs args)
    {
        LogoSceneScript.controller = true;
    }
    void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
    {
        LogoSceneScript.controller = false;
    }
    void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args)
    {
        LogoSceneScript.controller = false;
    }
    void OnDestroy()
    {
        ReInput.ControllerConnectedEvent -= OnControllerConnected;
        ReInput.ControllerDisconnectedEvent -= OnControllerDisconnected;
        ReInput.ControllerPreDisconnectEvent -= OnControllerPreDisconnect;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            SEManager.Instance.Play(SEPath.WINDOW_SE, 1);
            WindowOn.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SEManager.Instance.Play(SEPath.WINDOW_CLOSE_SE, 1);
            WindowOff.Invoke();
        }
    }

    /// <summary>
    /// コントローラーを繋いでいるかどうかで表示するテキストを変更
    /// </summary>
    public void WalkWindow()
    {
        if (!LogoSceneScript.controller)
            tutorialWindow.text = "Keyboard:右移動\n" + DataGetScript.getLeftWalk1() + " or " + DataGetScript.getLeftWalk2() + "\nKeyboard:左移動\n" + DataGetScript.getRightWalk1() + " or " + DataGetScript.getRightWalk2();
        else
            tutorialWindow.text = "Controller:移動\nL_Stick or D_Pad";
    }
    public void JumpWindow()
    {
        if (!LogoSceneScript.controller)
            tutorialWindow.text = "Keyboard:ジャンプ\n" + DataGetScript.getJump1() + " or " + DataGetScript.getJump2() + "\n箱を動かして\n足場にしよう";
        else
            tutorialWindow.text = "Controller:ジャンプ\n" + DataGetScript.getJump3() + "\n箱を動かして\n足場にしよう";
    }
    public void ModeChangeWindow()
    {
        if (!LogoSceneScript.controller)
            tutorialWindow.text = "Keyboard:火の点火:消火\n" + DataGetScript.getModeChange1() + " or " + DataGetScript.getModeChange2() + "\n火がついていると体力が\n減っていく代わりに\n周りが見やすくなる";
        else
            tutorialWindow.text = "Controller:火の点火:消火\n" + DataGetScript.getModeChange3() + "\n火がついていると体力が\n減っていく代わりに\n周りが見やすくなる";
    }
    public void FireWindow()
    {
        if (!LogoSceneScript.controller)
            tutorialWindow.text = "Keyboard:火の着火\n" + DataGetScript.getFire1() + " or " + DataGetScript.getFire2() + "\n火がついている状態で押すと\n燃やせる";
        else
            tutorialWindow.text = "Controller:火の着火\n" + DataGetScript.getFire3() + "\n火がついている状態で押すと\n燃やせる";
    }
    public void ShotWindow()
    {
        if (!LogoSceneScript.controller)
            tutorialWindow.text = "Keyboard:火を飛ばす\n" + DataGetScript.getShot1() + " or " + DataGetScript.getShot2() + "\n火がついている状態で押すと\n体力を使って火のついた\n蝋を飛ばせる";
        else
            tutorialWindow.text = "Controller:火を飛ばす\n" + DataGetScript.getShot3() + "\n火がついている状態で押すと\n体力を使って火のついた\n蝋を飛ばせる";
    }
    public void RoadWindow()
    {
        if (!LogoSceneScript.controller)
            tutorialWindow.text = "Keyboard:道を作る\n" + DataGetScript.getCrouching1() + " or " + DataGetScript.getCrouching2() + "\n火がついている状態で押すと\n体力の代わりに道を作る";
        else
            tutorialWindow.text = "Controller:道を作る\n" + DataGetScript.getCrouching3() + "\n火がついている状態で押すと\n体力の代わりに道を作る";
    }
}

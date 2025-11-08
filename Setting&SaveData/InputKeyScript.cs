using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.Frost;
using UnityEngine.Events;
using MBLDefine;
using System;
using TMPro;
using Rewired;

public class InputKeyScript : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private PressKey press;
    [SerializeField] private TextMeshProUGUI duplicateKeyText;      //エラーテキスト
    [SerializeField] private UnityEvent duplicateKey;       //重複した場合
    [SerializeField] private UnityEvent mousePointer;       //カーソル
    [SerializeField] private UnityEvent successfulAction;   //成功した場合
    [SerializeField] private UnityEvent gameScene;          //現在のシーン
    private Rewired.Player player0;
    private string saveKeyName;
    
    
    private void Awake()
    {
        press = press.GetComponent<PressKey>();
        inputManager = GetComponent<InputManager>();
        canvasGroup = canvasGroup.GetComponent<CanvasGroup>();
        duplicateKeyText = duplicateKeyText.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        player0 = ReInput.players.GetPlayer(0);
        gameScene.Invoke();
    }
    void Update()
    {
        if (canvasGroup.alpha==1)
        {
            //コントローラーのキー変更ボタンなら
            if(KeyNameScript.controller)
            {
                //コントローラーのボタンが押されたら
                if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Joystick1Button4) ||
                  Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Joystick1Button6) || Input.GetKey(KeyCode.Joystick1Button8) || Input.GetKey(KeyCode.Joystick1Button9) ||
                   Input.GetKey(KeyCode.Joystick1Button10) || Input.GetKey(KeyCode.Joystick1Button11) || Input.GetKey(KeyCode.Joystick1Button12) || Input.GetKey(KeyCode.Joystick1Button13) || Input.GetKey(KeyCode.Joystick1Button14) ||
                    Input.GetKey(KeyCode.Joystick1Button15) || Input.GetKey(KeyCode.Joystick1Button16) || Input.GetKey(KeyCode.Joystick1Button17) || Input.GetKey(KeyCode.Joystick1Button18) || Input.GetKey(KeyCode.Joystick1Button19))
                {
                    foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(code))
                        {
                            ControllerBindSet(code);
                            break;
                        }
                    }
                }
            }
            //キーボードの変更ボタンなら
            else
            {
                //コントローラーとマウス、エンターキー以外が押されたら
                if (Input.anyKey && !Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.Mouse2) && !Input.GetKey(KeyCode.Mouse3) && !Input.GetKey(KeyCode.Mouse4) && !Input.GetKey(KeyCode.Mouse5) && !Input.GetKey(KeyCode.Mouse6) &&
                 !Input.GetKey(KeyCode.Joystick1Button0) && !Input.GetKey(KeyCode.Joystick1Button1) && !Input.GetKey(KeyCode.Joystick1Button2) && !Input.GetKey(KeyCode.Joystick1Button3) && !Input.GetKey(KeyCode.Joystick1Button4) &&
                  !Input.GetKey(KeyCode.Joystick1Button5) && !Input.GetKey(KeyCode.Joystick1Button6) && !Input.GetKey(KeyCode.Joystick1Button7) && !Input.GetKey(KeyCode.Joystick1Button8) && !Input.GetKey(KeyCode.Joystick1Button9) &&
                   !Input.GetKey(KeyCode.Joystick1Button10) && !Input.GetKey(KeyCode.Joystick1Button11) && !Input.GetKey(KeyCode.Joystick1Button12) && !Input.GetKey(KeyCode.Joystick1Button13) && !Input.GetKey(KeyCode.Joystick1Button14) &&
                    !Input.GetKey(KeyCode.Joystick1Button15) && !Input.GetKey(KeyCode.Joystick1Button16) && !Input.GetKey(KeyCode.Joystick1Button17) && !Input.GetKey(KeyCode.Joystick1Button18) && !Input.GetKey(KeyCode.Joystick1Button19))
                {
                    foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(code))
                        {
                            KeyBindSet(code);
                            break;
                        }
                    }
                }
            }
        }
    }
    #region キー変更
    /// <summary>
    /// キーの名前変更とキーを変更
    /// </summary>
    /// <param name="code">キーの変更先</param>
    public void KeyBindSet(KeyCode code)
    {
        saveKeyName = code.ToString();
        if (saveKeyName == "Mouse0")
        {
            saveKeyName = "LeftClick";
        }
        if (saveKeyName == "Mouse1")
        {
            saveKeyName = "RightClick";
        }
        
        if (KeyNameScript.leftWalk == 1)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.LeftWalk1))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveLeftWalk1(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.leftWalk == 2)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.LeftWalk2))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveLeftWalk2(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.rightWalk == 1)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.RightWalk1))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveRightWalk1(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.rightWalk == 2)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.RightWalk2))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveRightWalk2(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.jump == 1)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Jump1))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveJump1(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.jump == 2)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Jump2))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveJump2(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.crouching == 1)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Crouching1))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveCrouching1(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.crouching == 2)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Crouching2))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveCrouching2(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.fire == 1)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Fire1))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveFire1(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.fire == 2)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Fire2))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveFire2(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.shot == 1)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Shot1))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveShot1(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.shot == 2)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Shot2))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveShot2(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.modeChange == 1)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.ModeChange1))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveModeChange1(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.modeChange == 2)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.ModeChange2))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveModeChange2(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        successfulAction.Invoke();
        KeyNameScript.leftWalk = 0;
        KeyNameScript.rightWalk = 0;
        KeyNameScript.jump = 0;
        KeyNameScript.crouching = 0;
        KeyNameScript.fire = 0;
        KeyNameScript.shot = 0;
        KeyNameScript.modeChange = 0;
    }
    #endregion
    #region コントローラーボタン変更
    /// <summary>
    /// コントローラーのボタン名前変更とコントローラーのボタン変更
    /// </summary>
    /// <param name="code">コントローラーのボタン変更先</param>
    public void ControllerBindSet(KeyCode code)
    {
        saveKeyName = code.ToString();
        #region 名前変更
        if (saveKeyName == "JoystickButton0")
        {
            saveKeyName = "A Button";
        }
        if (saveKeyName == "JoystickButton1")
        {
            saveKeyName = "B Button";
        }
        if (saveKeyName == "JoystickButton2")
        {
            saveKeyName = "X Button";
        }
        if (saveKeyName == "JoystickButton3")
        {
            saveKeyName = "Y Button";
        }

        if (saveKeyName == "JoystickButton4")
        {
            saveKeyName = "LB";
        }
        if (saveKeyName == "JoystickButton5")
        {
            saveKeyName = "RB";
        }

        if (saveKeyName == "JoystickButton6")
        {
            saveKeyName = "Back Button";
        }
        if (saveKeyName == "JoystickButton7")
        {
            saveKeyName = "Start Button";
        }

        if (saveKeyName == "JoystickButton8")
        {
            saveKeyName = "Left Stick Button";
        }
        if (saveKeyName == "JoystickButton9")
        {
            saveKeyName = "Right Stick Button";
        }
        #endregion
        if (KeyNameScript.jump == 3)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Jump3))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveJump3(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.crouching == 3)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Crouching3))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveCrouching3(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.fire == 3)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Fire3))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveFire3(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.shot == 3)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.Shot3))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveShot3(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        else if (KeyNameScript.modeChange == 3)
        {
            if (InputManager.KeyConfigSetting.Instance.SetCurrentKey(Key.ModeChange3))
            {
                InputManager.KeyConfigSetting.Instance.SaveSetting();
                SaveManager.SaveModeChange3(saveKeyName);
            }
            else
            {
                DuplicateError(saveKeyName);
                return;
            }
        }
        successfulAction.Invoke();
        KeyNameScript.leftWalk = 0;
        KeyNameScript.rightWalk = 0;
        KeyNameScript.jump = 0;
        KeyNameScript.crouching = 0;
        KeyNameScript.fire = 0;
        KeyNameScript.shot = 0;
        KeyNameScript.modeChange = 0;
    }
    #endregion
    /// <summary>
    /// 重複した場合エラーが表示されるように
    /// </summary>
    /// <param name="keyName"></param>
    public void DuplicateError(string keyName)
    {
        duplicateKey.Invoke();
        duplicateKeyText.text = keyName + " is a Duplicate Key";
    }
    /// <summary>
    /// ゲームシーンではマウスカーソルを表示しないように
    /// </summary>
    public void GameChange()
    {
        player0.controllers.maps.SetMapsEnabled(true, "Game");
        player0.controllers.maps.SetMapsEnabled(false, "Default");
    }
    /// <summary>
    /// UIが出ている状態ではマウスカーソルを表示するように
    /// </summary>
    public void UIChange()
    {
        player0.controllers.maps.SetMapsEnabled(false, "Game");
        player0.controllers.maps.SetMapsEnabled(true, "Default");
    }
}

using MBLDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 入力を管理する
/// </summary>
[System.Serializable]
internal class InputManager : SingletonMonoBehaviour<InputManager>
{
    #region InnerClass

    /// <summary>
    /// キーコンフィグ設定の変更や保存・ロードを管理する
    /// </summary>
    [System.Serializable]
    internal class KeyConfigSetting
    {
        [SerializeField]private Array keyCodeValues;
        [SerializeField]private static InputManager inputManager;
        [SerializeField]private static KeyConfigSetting instance;
        string saveKeyName;

        public static KeyConfigSetting Instance
        {
            get
            {
                if (inputManager == null)
                    inputManager = InputManager.Instance;
                return instance = instance != null ? instance : new KeyConfigSetting();
            }
        }

        private KeyConfigSetting()
        {
        }

        /// <summary>
        /// 呼び出されたフレームで押下状態のKeyCodeリストを返す
        /// </summary>
        /// <returns>押下状態のKeyCodeリスト</returns>
        private List<KeyCode> GetCurrentInputKeyCode()
        {
            List<KeyCode> ret = new List<KeyCode>();
            if (keyCodeValues == null)
                keyCodeValues = Enum.GetValues(typeof(KeyCode));
            foreach (var code in keyCodeValues)
                if (Input.GetKey((KeyCode)(int)code))
                    ret.Add((KeyCode)(int)code);
            return ret;
        }

        /// <summary>
        /// コンフィグにキーをセットする
        /// </summary>
        /// <param name="key">キーを表す識別子</param>
        /// <param name="keyCode">割り当てるキーコード</param>
        /// <returns>割り当てが正常に終了したかどうか</returns>
        public bool SetKey(Key key, List<KeyCode> keyCode)
        {
            return inputManager.keyConfig.SetKey(key.String, keyCode);
        }

        /// <summary>
        /// コンフィグから値を消去
        /// </summary>
        /// <param name="key">キーを表す識別子</param>
        /// <returns>値の消去が正常に終了したかどうか</returns>
        public bool RemoveKey(Key key)
        {
            return inputManager.keyConfig.RemoveKey(key.String);
        }

        /// <summary>
        /// 押されているキーを名前文字列に対するキーとして設定する
        /// </summary>
        /// <param name="key">キーに割り付ける名前</param>
        /// <returns>キーコードの設定が正常に完了したかどうか</returns>
        public bool SetCurrentKey(Key key)
        {
            //HACK:マウス入力も受け付けるようにするべきなので今後改善
            //マウス{0~6}の入力を弾く
            var currentInput = GetCurrentInputKeyCode().Where(c => c < KeyCode.Mouse2 || KeyCode.Mouse6 < c).ToList();

            if (currentInput == null || currentInput.Count < 1)
            {

                return false;
            }
                
            //var code = inputManager.keyConfig.GetKeyCode(key.String);
            //既に設定されているキーと一部でも同じキーが押されている場合
            //Debug.Log(code.Count > currentInput.Count && currentInput.All(k => code.Contains(k)));
            //if (code.Count > currentInput.Count && currentInput.All(k => code.Contains(k)))
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    NameChange(code);

                    if (saveKeyName == SaveManager.sd.leftWalk1 || saveKeyName == SaveManager.sd.leftWalk2 || saveKeyName == SaveManager.sd.rightWalk1 || saveKeyName == SaveManager.sd.rightWalk2 ||
                        saveKeyName == SaveManager.sd.jump1 || saveKeyName == SaveManager.sd.jump2 || saveKeyName == SaveManager.sd.crouching1 || saveKeyName == SaveManager.sd.crouching2 ||
                        saveKeyName == SaveManager.sd.fire1 || saveKeyName == SaveManager.sd.fire2 || saveKeyName == SaveManager.sd.shot1 || saveKeyName == SaveManager.sd.shot2 ||
                        saveKeyName == SaveManager.sd.modeChange1 || saveKeyName == SaveManager.sd.modeChange1 ||
                        saveKeyName == SaveManager.sd.jump3 || saveKeyName == SaveManager.sd.crouching3 || saveKeyName == SaveManager.sd.fire3 || saveKeyName == SaveManager.sd.shot3 || saveKeyName == SaveManager.sd.modeChange3)
                    {
                        return false;
                    }
                }
                    
            }
            RemoveKey(key);
            return SetKey(key, currentInput);
        }

        /// <summary>
        /// デフォルトのキー設定を適用する
        /// </summary>
        public void SetDefaultKeyConfig()
        {
            foreach (var key in Key.AllKeyData)
                SetKey(key, key.DefaultKeyCode);
        }

        public List<KeyCode> GetKeyCode(Key keyName)
        {
            return inputManager.keyConfig.GetKeyCode(keyName.String);
        }

        public void LoadSetting()
        {
            InputManager.Instance.keyConfig.LoadConfigFile();
        }

        public void SaveSetting()
        {
            InputManager.Instance.keyConfig.SaveConfigFile();
        }
        public void NameChange(KeyCode code)
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
                saveKeyName = "Left Button";
            }
            if (saveKeyName == "JoystickButton5")
            {
                saveKeyName = "Right Button";
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
        }
    }

    #endregion InnerClass

    /// <summary>
    /// 使用するキーコンフィグ
    /// </summary>
    [SerializeField] private KeyConfig keyConfig = new KeyConfig(ExternalFilePath.KEYCONFIG_PATH);
    private const string PATHFILE = "データ初期化";
    private const string KEYFILE = "keyconf.dat";

    public void Awake()
    {
        DefaultAwake();
    }

    /// <summary>
    /// 指定したキーが押下状態かどうかを返す
    /// </summary>
    /// <returns>入力状態</returns>
    internal bool GetKey(Key key)
    {
        return keyConfig.GetKey(key.String);
    }

    /// <summary>
    /// 指定したキーが入力されたかどうかを返す
    /// </summary>
    /// <returns>入力状態</returns>
    internal bool GetKeyDown(Key key)
    {
        return keyConfig.GetKeyDown(key.String);
    }

    /// <summary>
    /// 指定したキーが離されたかどうかを返す
    /// </summary>
    /// <returns>入力状態</returns>
    internal bool GetKeyUp(Key key)
    {
        return keyConfig.GetKeyUp(key.String);
    }

    /// <summary>
    /// 軸入力に対する値を返す
    /// </summary>
    /// <returns>入力値</returns>
    internal float GetAxes(Axes axes)
    {
        return Input.GetAxis(axes.String);
    }

    /// <summary>
    /// 軸入力に対する値を返す
    /// </summary>
    /// <returns>平滑化フィルターが適用されていない入力値</returns>
    internal float GetAxesRaw(Axes axes)
    {
        return Input.GetAxisRaw(axes.String);
    }

    public void DefaultAwake()
    {
        //FirstStartScript.ExampleStart();
        SaveManager.load();
        ExampleStart();
        //InputManager.Instance.keyConfig.SaveConfigFile();
        Debug.Log("Load key-config file.");

        //最初はデフォルトの設定をコンフィグに格納
        KeyConfigSetting.Instance.SetDefaultKeyConfig();

        //コンフィグファイルがあれば読み出す
        try
        {
            InputManager.Instance.keyConfig.LoadConfigFile();
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
    }

    public void ExampleStart()
    {
        string path = Directory.GetCurrentDirectory();
        string keyconfig = Directory.GetCurrentDirectory();

        if (File.Exists(path += ("/" + PATHFILE)) == false)
        {
            File.Create(path);
            File.Delete(keyconfig += ("/"+KEYFILE));
            Debug.Log("初回起動");

            //InputManager.KeyConfigSetting.Instance.SetDefaultKeyConfig();
            FirstLoad();
            //Debug.Log("2回目以降の起動");
            return;
        }

        Debug.Log("2回目以降の起動");
    }
    public void FirstLoad()
    {
        SaveManager.saveStageStar();
        SaveManager.saveStageHiddenReset();
        SaveManager.saveClearStage(0);
        SaveManager.saveStageGenerate(9);
        SaveManager.SaveLeftWalk1("A");
        SaveManager.SaveLeftWalk2("LeftArrow");
        SaveManager.SaveRightWalk1("D");
        SaveManager.SaveRightWalk2("RightArrow");
        SaveManager.SaveCrouching1("S");
        SaveManager.SaveCrouching2("DownArrow");
        SaveManager.SaveCrouching3("X Button");
        SaveManager.SaveJump1("Space");
        SaveManager.SaveJump2("");
        SaveManager.SaveJump3("A Button");
        SaveManager.SaveShot1("LeftClick");
        SaveManager.SaveShot2("");
        SaveManager.SaveShot3("RB");
        SaveManager.SaveFire1("RightClick");
        SaveManager.SaveFire2("");
        SaveManager.SaveFire3("LB");
        SaveManager.SaveModeChange1("LeftShift");
        SaveManager.SaveModeChange2("RightShift");
        SaveManager.SaveModeChange3("B Button");
        SaveManager.SaveMaster(0.5f);
        SaveManager.SaveBGM(0.75f);
        SaveManager.SaveSE(0.5f);
        SaveManager.SaveHighlight(false);
        SaveManager.SaveHUD(true);
        SaveManager.SaveTutorial(true);
    }
}
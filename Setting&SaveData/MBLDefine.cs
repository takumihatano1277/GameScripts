using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("UnitTest")]

/// <summary>
/// このゲームで定義する定数などを扱う
/// </summary>
namespace MBLDefine
{
    /// <summary>
    /// 外部ファイルへの参照に必要なパス群
    /// </summary>
    internal struct ExternalFilePath
    {
        internal const string KEYCONFIG_PATH = "keyconf.dat";
    }

    /// <summary>
    /// 入力値の基底クラス
    /// </summary>
    internal class InputValue
    {
        public readonly string String;

        protected InputValue(string name)
        {
            String = name;
        }
    }

    /// <summary>
    /// 使用するキーを表すクラス
    /// </summary>
    internal sealed class Key : InputValue
    {
        public readonly List<KeyCode> DefaultKeyCode;
        public readonly static List<Key> AllKeyData = new List<Key>();

        private Key(string keyName, List<KeyCode> defaultKeyCode)
            : base(keyName)
        {
            DefaultKeyCode = defaultKeyCode;
            AllKeyData.Add(this);
        }

        public override string ToString()
        {
            return String;
        }

        public static readonly Key LeftWalk1 = new Key("LeftWalk1", new List<KeyCode> { KeyCode.A });
        public static readonly Key LeftWalk2 = new Key("LeftWalk2", new List<KeyCode> { KeyCode.LeftArrow });
        public static readonly Key RightWalk1 = new Key("RightWalk1", new List<KeyCode> { KeyCode.D });
        public static readonly Key RightWalk2 = new Key("RightWalk2", new List<KeyCode> { KeyCode.RightArrow });
        public static readonly Key Jump1 = new Key("Jump1", new List<KeyCode> { KeyCode.Space });
        public static readonly Key Jump2 = new Key("Jump2", new List<KeyCode> { KeyCode.Space });
        public static readonly Key Crouching1 = new Key("Crouching1", new List<KeyCode> { KeyCode.S });
        public static readonly Key Crouching2 = new Key("Crouching2", new List<KeyCode> { KeyCode.DownArrow });
        public static readonly Key Shot1 = new Key("Shot1", new List<KeyCode> { KeyCode.Mouse0 });
        public static readonly Key Shot2 = new Key("Shot2", new List<KeyCode> { KeyCode.Mouse0 });
        public static readonly Key Fire1 = new Key("Fire1", new List<KeyCode> { KeyCode.Mouse1 });
        public static readonly Key Fire2 = new Key("Fire2", new List<KeyCode> { KeyCode.Mouse1 });
        public static readonly Key ModeChange1 = new Key("ModeChange1", new List<KeyCode> { KeyCode.LeftShift });
        public static readonly Key ModeChange2 = new Key("ModeChange2", new List<KeyCode> { KeyCode.RightShift });
        
        public static readonly Key Jump3 = new Key("Jump3", new List<KeyCode> { KeyCode.Joystick1Button0 });
        public static readonly Key Crouching3 = new Key("Crouching3", new List<KeyCode> { KeyCode.Joystick1Button2 });
        public static readonly Key Shot3 = new Key("Shot3", new List<KeyCode> { KeyCode.Joystick1Button5 });
        public static readonly Key Fire3 = new Key("Fire3", new List<KeyCode> { KeyCode.Joystick1Button4 });
        public static readonly Key ModeChange3 = new Key("ModeChange3", new List<KeyCode> { KeyCode.Joystick1Button1 });

        //public static readonly Key Menu = new Key("Menu", new List<KeyCode> { KeyCode.Escape });
    }

    /// <summary>
    /// 使用する軸入力を表すクラス
    /// </summary>
    internal sealed class Axes : InputValue
    {
        public readonly static List<Axes> AllAxesData = new List<Axes>();

        private Axes(string axesName)
            : base(axesName)
        {
            AllAxesData.Add(this);
        }

        public override string ToString()
        {
            return String;
        }

        public static Axes Horizontal = new Axes("Horizontal");
        public static Axes Vertical = new Axes("Vertical");
        public static Axes L_Stick_H = new Axes("L_Stick_H");
        public static Axes L_Stick_V = new Axes("L_Stick_V");
        public static Axes D_Pad_H = new Axes("D_Pad_H");
        public static Axes D_Pad_V = new Axes("D_Pad_V");
    }
}
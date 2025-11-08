using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResultPlayerScript : MonoBehaviour
{
    [SerializeField] private UnityEvent AnimationMotion;        //ゲームオーバーシーンでは死亡モーション・クリアシーンでは待機モーション
    // Start is called before the first frame update
    void Start()
    {
        AnimationMotion.Invoke();
    }
}

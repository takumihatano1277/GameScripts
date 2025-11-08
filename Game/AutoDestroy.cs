using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 割り当てしてからtime秒で削除
/// </summary>
public class AutoDestroy : MonoBehaviour
{
    public float time { get; internal set; }

    void Start()
    {
        Destroy(gameObject, time);      //スクリプトを当てた時に時間を指定するとそこからその時間がたった他後削除
    }
}

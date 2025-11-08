using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

/// <summary>
/// 木箱
/// </summary>
public class BoxScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Road")        //箱が地面に落ちたら
        {
            SEManager.Instance.Play(SEPath.IMPACT_WOOD_PLANK_ON_WOOD_PILE_11_MONO, 1);      //木箱が地面に落ちたSE
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class FireShotScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var fireTarget = other.gameObject.GetComponent<IIgnitioable>();     //燃えるものに当たった時
        if (fireTarget != null)
        {
            fireTarget.FireOn();
        }
        var water = other.gameObject.GetComponent<IWateable>();             //水に当たった時
        if (water != null)
        {
            water.WaterExplosion();
        }
        var torch = other.gameObject.GetComponent<ITorcable>();             //松明に当たった時
        if (torch != null)
        {
            torch.TorchFire();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Road")
        {
            SEManager.Instance.Play(SEPath.IMPACT_STONE_ON_STONE_01_SUBTLE_MONO, 1);        //地面に落ちた時にSE
        }
        var fireTarget = collision.gameObject.GetComponent<IIgnitioable>();
        if (fireTarget != null)
        {
            fireTarget.FireOn();            //燃えるものに当たった時
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 電撃トラップ
/// </summary>
public class BoltTrapScript : MonoBehaviour
{
    LineRenderer line;
    BoxCollider box;
    AudioSource audio;
    void Start()
    {
        line = transform.parent.GetComponent<LineRenderer>();
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Box")     //箱でふさがれた
        {
            line.enabled = false;
            audio.Stop();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (line.enabled)
        {
            var TrapTarget = other.gameObject.GetComponent<ITrapable>();
            if (TrapTarget != null)
            {
                TrapTarget.TrapAttack();        //トラップに当たった
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Box")
        {
            line.enabled = true;
            audio.Play();
        }
    }
}

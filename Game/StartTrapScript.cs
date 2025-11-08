using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using KanKikuchi.AudioManager;

public class StartTrapScript : MonoBehaviour
{
    [SerializeField] private UnityEvent trap;       //トラップを踏んだ時に起こるイベント
    private bool first;
    private void OnTriggerEnter(Collider other)
    {
        if(!first && other.gameObject.tag == "Player")
        {
            first = true;
            SEManager.Instance.Play(SEPath.SWITCH);
            trap.Invoke();
        }
    }
}

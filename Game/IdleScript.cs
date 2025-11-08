using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 待機モーション
/// </summary>
public class IdleScript : MonoBehaviour
{
    [SerializeField] private float time = 0;
    [SerializeField] private UnityEvent motion;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = transform.GetChild(0).GetComponent<AudioSource>();
        Invoke("TimeMotion", time);
    }
    public void TimeMotion()
    {
        motion.Invoke();
    }
    public void MoveStone()
    {
        audio.Play();
    }
}

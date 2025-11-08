using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 迫る壁のトラップ
/// </summary>
public class BladeTrapScript : MonoBehaviour
{
    [SerializeField] private float speed = 1;       //速度
    private Vector3 velocity;
    private bool isStart;                             //壁のスタート開始

    [SerializeField] private float timer = 1f;      //1秒ごとに
    private float currentTime = 0f;                 //時間
    [SerializeField] private float accele = 10.0f;  //加速度
    void Start()
    {
        velocity = gameObject.transform.rotation * new Vector3(speed, 0, 0);        //動いていく方向
    }
    void Update()
    {
        if(!GameManagerScript.IsWallStop() && isStart)          //トラップを踏んでいれば
        {
            currentTime += Time.deltaTime;

            if (currentTime > timer)        //1秒を超えていたら
            {
                velocity = gameObject.transform.rotation * new Vector3(speed, 0, 0);
                speed += accele;        //加速
                currentTime = 0f;
            }
            gameObject.transform.position += velocity * Time.deltaTime;
        }
    }
    /// <summary>
    /// ほかのscriptから参照
    /// </summary>
    /// <param name="_start"></param>
    public void TrapStart(bool _start)
    {
        isStart = _start;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
using MBLDefine;

public class PlayerCameraScript : MonoBehaviour
{
    public Transform target;
    private float smoothing = 5f;       //変換速度
    private float angle = 10.0f;        //どれくらいの角度をつけるか
    private Vector3 offset;
    private float cameraRotate = 0.5f;
    private bool pause;
    [SerializeField] private float bottom = 30.0f;      //ステージによって変更
    [SerializeField] private float top = 85.0f;         //ステージによって変更
    [SerializeField] private float last = 1530.0f;      //ステージによって変更

    [SerializeField] private InputManager inputManager;
    private void Awake()
    {
        offset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        if(!GameManagerScript.IsPaused())      //ポーズ状態ではない
        {
            Vector3 targetCamPos = target.position + offset;
            if (!GameManagerScript.IsCameraStop())     //カメラを制限されていない
            {
                if (inputManager.GetKey(Key.RightWalk1) || inputManager.GetKey(Key.RightWalk2) ||
                    inputManager.GetAxes(Axes.L_Stick_H) >= 0.1f || inputManager.GetAxes(Axes.D_Pad_H) >= 0.1f)     //右移動をしていたら
                {
                    var mainTarget = Quaternion.Euler(new Vector3(0, angle, 0));        //右奥視点に
                    var now_rot = transform.rotation;
                    if (Quaternion.Angle(now_rot, mainTarget) <= 1)
                        transform.rotation = mainTarget;
                    else
                        transform.Rotate(new Vector3(0, cameraRotate, 0));
                }

                if (inputManager.GetKey(Key.LeftWalk1) || inputManager.GetKey(Key.LeftWalk2) ||
                    inputManager.GetAxes(Axes.L_Stick_H) <= -0.1f || inputManager.GetAxes(Axes.D_Pad_H) <= -0.1f)   //左移動していたら
                {
                    var mainTarget = Quaternion.Euler(new Vector3(0, -angle, 0));       //左奥視点に
                    var now_rot = transform.rotation;
                    if (Quaternion.Angle(now_rot, mainTarget) <= 1)
                        transform.rotation = mainTarget;
                    else
                        transform.Rotate(new Vector3(0, -cameraRotate, 0));
                }
            }
            if (targetCamPos.x <= 0.0f)     //ここより先左に行かないように
                targetCamPos.x = 0.0f;
            if (targetCamPos.x >= last)     //ここより先右に行かないように
                targetCamPos.x = last;
            if (targetCamPos.y <= bottom)   //ここより先下に行かないように
                targetCamPos.y = bottom;
            if (targetCamPos.y >= top)      //ここより先上に行かないように
                targetCamPos.y = top;
            transform.position = Vector3.Lerp(
                transform.position,
                targetCamPos,
                Time.deltaTime * smoothing
            );
        }
    }
}
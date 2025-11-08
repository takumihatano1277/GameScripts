using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ずっと右に移動するのを追い続ける
/// </summary>
public class TitleCameraScript : MonoBehaviour
{
    public Transform target;
    private float smoothing = 5f;
    private Vector3 offset;
    private float cameraRotate = 0.5f;
    private float angle = 10.0f;        //どれくらいの角度をつけるか
    private float bottom = 30.0f;      //ステージによって変更
    private float top = 85.0f;         //ステージによって変更

    void Start()
    {
        offset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        if (transform.position.x < targetCamPos.x)
        {
            var mainTarget = Quaternion.Euler(new Vector3(0, angle, 0));
            var now_rot = transform.rotation;
            if (Quaternion.Angle(now_rot, mainTarget) <= 1)
                transform.rotation = mainTarget;
            else
                transform.Rotate(new Vector3(0, cameraRotate, 0));
        }
        if (transform.position.x > targetCamPos.x)
        {
            var mainTarget = Quaternion.Euler(new Vector3(0, -angle, 0));
            var now_rot = transform.rotation;
            if (Quaternion.Angle(now_rot, mainTarget) <= 1)
                transform.rotation = mainTarget;
            else
                transform.Rotate(new Vector3(0, -cameraRotate, 0));
        }
        
        if (targetCamPos.y <= bottom)
            targetCamPos.y = bottom;
        if (targetCamPos.y >= top)
            targetCamPos.y = top;
        transform.position = Vector3.Lerp(
            transform.position,
            targetCamPos,
            Time.deltaTime * smoothing
        );
    }
}
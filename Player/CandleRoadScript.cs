using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleRoadScript : MonoBehaviour
{
    private float destroyTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);      //5秒後に蝋の道を削除
    }
}

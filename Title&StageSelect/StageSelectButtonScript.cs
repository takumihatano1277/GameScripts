using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 現在の進行度からステージ開放
/// </summary>
public class StageSelectButtonScript : MonoBehaviour
{
    private int stageCount = 7;
    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript.SavePoint(false);
        for(int i=0;i<=SaveManager.sd.clearStage;i++)
        {
            if (i >= stageCount)
            {
                return;
            }
            transform.GetChild(i+1).gameObject.SetActive(true);
        }
    }
}

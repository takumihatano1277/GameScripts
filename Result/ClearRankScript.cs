using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearRankScript : MonoBehaviour
{
    [SerializeField] private List<Image> fireRank;
    private CanvasGroup canvas;
    private bool rankSet;
    float alpha;

    string blue = "fireblue";
    string green = "firegreen";
    string red = "firered";
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.parent.gameObject.GetComponent<CanvasGroup>();
        foreach(Transform image in gameObject.transform)
        {
            fireRank.Add(image.gameObject.GetComponent<Image>());
        }

        SaveManager.saveStage(GameManagerScript.blueCheck, GameManagerScript.greenCheck, GameManagerScript.redCheck, GameManagerScript.stageNo +1);      //クリアしたステージの進行度を保存

        if (!GameManagerScript.redCheck)       //赤い松明を灯していなかったらアニメーションせずに削除
        {
            fireRank.RemoveAt(2);
        }
        if (!GameManagerScript.greenCheck)     //緑の松明を灯していなかったらアニメーションせずに削除
        {
            fireRank.RemoveAt(1);
        }
        if (!GameManagerScript.blueCheck)      //青い松明を灯していなかったらアニメーションせずに削除
        {
            fireRank.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!rankSet)
        {
            if (canvas.alpha == 1)
            {
                foreach (Image fire in fireRank)        //灯している松明をフェードイン
                {
                    alpha += Time.unscaledDeltaTime / 2;
                    if (alpha >= 1.0f)
                    {
                        Debug.Log(fire.name);
                        alpha = 1.0f;
                        rankSet = true;
                    }

                    //フェード用Imageの色・透明度設定
                    fire.color = new Color(1.0f, 1.0f, 1.0f, alpha);
                }
            }
        }
    }
}

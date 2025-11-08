using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectRankScript : MonoBehaviour
{
    [SerializeField] private List<Image> fireRank;
    private CanvasGroup canvas;
    private bool rankSet;
    float alpha;

    [SerializeField] Image star;

    [SerializeField] private Image[] fireFrame;

    string blue = "fireblue";
    string green = "firegreen";
    string red = "firered";
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.parent.gameObject.GetComponent<CanvasGroup>();
        foreach (Transform image in gameObject.transform)
        {
            fireRank.Add(image.gameObject.GetComponent<Image>());
        }
        RankCheck(1);
    }
    /// <summary>
    /// クリア時のランクを判定
    /// </summary>
    /// <param name="_rank">ステージの番号</param>
    public void RankCheck(int _rank)
    {
        foreach (Image fire in fireRank)
        {
            //フェード用Imageの色・透明度設定
            fire.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        fireRank.Clear();
        foreach (Transform image in gameObject.transform)
        {
            fireRank.Add(image.gameObject.GetComponent<Image>());
        }
        if(_rank>=5)
        {
            if(SaveManager.sd.stageStar[_rank-4])
            {
                star.gameObject.SetActive(true);
            }
            if (!SaveManager.sd.stageStar[_rank - 4])
            {
                star.gameObject.SetActive(false);
            }
        }
        else
        {
            star.gameObject.SetActive(false);
        }
        if(_rank<=3)
        {
            for(int i=0;i<fireFrame.Length;i++)
            {
                fireFrame[i].enabled = false;
            }
            fireRank.RemoveAt(2);
            fireRank.RemoveAt(1);
            fireRank.RemoveAt(0);
        }
        else
        {
            for (int i = 0; i < fireFrame.Length; i++)
            {
                fireFrame[i].enabled = true;
            }
            if (!SaveManager.sd.stageData[_rank * 3])
            {
                fireRank.RemoveAt(2);
            }
            if (!SaveManager.sd.stageData[_rank * 3 - 1])
            {
                fireRank.RemoveAt(1);
            }
            if (!SaveManager.sd.stageData[_rank * 3 - 2])
            {
                fireRank.RemoveAt(0);
            }
            Debug.Log(_rank * 3);
            Debug.Log(_rank * 3 - 1);
            Debug.Log(_rank * 3 - 2);
        }
        rankSet = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!rankSet && canvas.alpha == 1)
        {
            foreach (Image fire in fireRank)
            {

                alpha += Time.unscaledDeltaTime / 2;

                //フェードアウト終了判定
                if (alpha >= 1.0f)
                {
                    alpha = 1.0f;
                    rankSet = true;
                }

                //フェード用Imageの色・透明度設定
                fire.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            }
        }
    }
}

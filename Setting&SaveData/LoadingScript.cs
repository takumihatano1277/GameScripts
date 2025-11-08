using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

public class LoadingScript : MonoBehaviour
{
    //フェード用のCanvasとImage
    private CanvasGroup canvasGroup;
    private Image fadeImage;
    private float alpha = 0.0f;     //フェード用Imageの透明度
    //フェードインアウトのフラグ
    private bool isFadeIn = false;
    private bool isFadeOut = false;
    private bool loadingFade;
    [SerializeField] private GameObject nextButton;
    private float fadeTime = 1.0f;      //フェードしたい時間（単位は秒）
    private int nextScene = 1;      //遷移先のシーン番号
    private AsyncOperation async;
    static public int reStartNo;
    [SerializeField] private GameObject loadUI;     //　シーンロード中に表示するUI画面
    [SerializeField] private GameObject thank;
    [SerializeField] private GameObject secret;
    [SerializeField] private GameObject gameClear;
    [SerializeField] private GameObject lastClear;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        fadeImage = transform.GetChild(0).gameObject.GetComponent<Image>();
        if (reStartNo == 12 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            gameClear.SetActive(false);
            lastClear.SetActive(true);
            thank.gameObject.SetActive(true);
            secret.gameObject.SetActive(true);
            nextButton.SetActive(false);
        }
    }
    /// <summary>
    /// 再スタートロード
    /// </summary>
    public void ReStartFadeOut()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameManagerScript.SavePoint(false);
        }
        nextScene = reStartNo;
        fadeImage.raycastTarget = true;
        loadingFade = true;
    }
    /// <summary>
    /// 次のステージへロード移動
    /// </summary>
    public void NextStageFadeOut()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameManagerScript.SavePoint(false);
        }
        if(reStartNo==7)
        {
            reStartNo++;
        }
        nextScene = reStartNo + 1;
        fadeImage.raycastTarget = true;
        loadingFade = true;
    }
    /// <summary>
    /// ステージセレクトへロード移動
    /// </summary>
    public void StageSelectFadeOut()
    {
        nextScene = 4;
        fadeImage.raycastTarget = true;
        loadingFade = true;
    }
    void Update()
    {
        if (loadingFade)
        {
            canvasGroup.alpha += Time.deltaTime / fadeTime;     //経過時間から透明度計算
            if (canvasGroup.alpha >= 1.0f)      //フェードアウト終了判定
            {
                loadingFade = false;
                canvasGroup.alpha = 1.0f;
                BGMSwitcher.FadeOutAndFadeIn(BGMPath.BGM01, 2, 1, 1, 0, 1, true);
                StartCoroutine("LoadData", nextScene);      //次のシーンへ遷移
            }
        }
    }
    IEnumerator LoadData(int n)
    {
        yield return new WaitForSeconds(0.5f);
        async = SceneManager.LoadSceneAsync(n);         //シーンの読み込みをする
        while (!async.isDone)                           //読み込みが終わるまで進捗状況をスライダーの値に反映させる
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            yield return null;
        }
    }
}

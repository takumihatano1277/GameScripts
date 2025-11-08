using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent Event;
    [SerializeField] private TextFx.TextFxTextMeshProUGUI textFx;
    [SerializeField] private TextFx.TextFxTextMeshProUGUI stageNameText;
    CanvasGroup canvas;
    private int stageNo = 0;

    //フェード用のCanvasとImage
    [SerializeField] private CanvasGroup canvasGroup;
    private Image fadeImage;

    //フェード用Imageの透明度
    private float alpha = 0.0f;

    //フェードインアウトのフラグ
    private bool isFadeIn = false;
    private bool isFadeOut = false;

    bool loadingFade;

    //フェードしたい時間（単位は秒）
    private float fadeTime = 1.0f;

    //遷移先のシーン番号
    private int nextScene = 1;

    private AsyncOperation async;
    //　シーンロード中に表示するUI画面
    [SerializeField]
    private GameObject loadUI;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = canvasGroup.GetComponent<CanvasGroup>();
        fadeImage = canvasGroup.transform.GetChild(0).gameObject.GetComponent<Image>();
        canvas = GetComponent<CanvasGroup>();
        textFx = textFx.GetComponent<TextFx.TextFxTextMeshProUGUI>();
        stageNameText = stageNameText.GetComponent<TextFx.TextFxTextMeshProUGUI>();
        Invoke("OnEvent", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingFade)
        {
            //経過時間から透明度計算
            canvasGroup.alpha += Time.deltaTime / fadeTime;

            //フェードアウト終了判定
            if (canvasGroup.alpha >= 1.0f)
            {
                loadingFade = false;
                canvasGroup.alpha = 1.0f;

                //次のシーンへ遷移
                StartCoroutine("LoadData", nextScene);
            }

        }
    }
    IEnumerator LoadData(int n)
    {
        yield return new WaitForSeconds(0.5f);

        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync(n);
        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            //slider.value = progressVal;
            yield return null;
        }
    }
    /// <summary>
    /// ボタンを押した後に呼び出し
    /// </summary>
    public void OnEvent()
    {
        Event.Invoke();
        textFx.AnimationManager.PlayAnimation(0.5f);
        stageNameText.AnimationManager.PlayAnimation(0.5f);
    }
    /// <summary>
    /// ステージ移動時に呼び出し
    /// </summary>
    public void StageTransition()
    {
        nextScene = stageNo+5;
        fadeImage.raycastTarget = true;
        loadingFade = true;
    }
    /// <summary>
    /// ステージセレクトボタンを押したときに呼び出し
    /// </summary>
    /// <param name="_stage">ボタンの番号</param>
    public void StageSelect(int _stage)
    {
        switch (_stage)
        {
            case 0:
                textFx.text = "Tutorial l";
                stageNameText.enabled = true;
                stageNameText.text = "操作説明";
                stageNo = 0;
                break;
            case 1:
                textFx.text = "Tutorial ll";
                stageNameText.enabled = true;
                stageNameText.text = "ギミック";
                stageNo = 1;
                break;
            case 2:
                textFx.text = "Tutorial lll";
                stageNameText.enabled = true;
                stageNameText.text = "ギミック&道作成";
                stageNo = 2;
                break;
            case 3:
                textFx.text = "Tutorial";
                stageNameText.enabled = false;
                stageNameText.text = "";
                stageNo = 3;
                break;
            case 4:
                textFx.text = "Stage l";
                stageNameText.enabled = false;
                stageNameText.text = "";
                stageNo = 4;
                break;
            case 5:
                textFx.text = "Stage ll";
                stageNameText.enabled = false;
                stageNameText.text = "";
                stageNo = 5;
                break;
            case 6:
                textFx.text = "Stage lll";
                stageNameText.enabled = false;
                stageNameText.text = "";
                stageNo = 6;
                break;
            case 7:
                textFx.text = "Stage lV";
                stageNameText.enabled = false;
                stageNameText.text = "";
                stageNo = 7;
                break;
            case 8:
                textFx.text = "Stage V";
                stageNameText.enabled = false;
                stageNameText.text = "";
                stageNo = 8;
                break;
        }
        OnEvent();
    }
}

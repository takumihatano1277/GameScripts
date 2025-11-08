using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    //フェード用のCanvasとImage
    private Canvas fadeCanvas;
    private Image fadeImage;
    //フェード用Imageの透明度
    private float alpha = 0.0f;
    //フェードインアウトのフラグ
    private bool isFadeIn = false;
    private bool isFadeOut = false;
    //フェードしたい時間（単位は秒）
    private float fadeTime = 1.0f;
    //遷移先のシーン番号
    private static int nextScene = 1;

    void Init()
    {
        //フェード用のCanvas生成
        GameObject FadeCanvasObject = new GameObject("CanvasFade");
        fadeCanvas = FadeCanvasObject.AddComponent<Canvas>();
        FadeCanvasObject.AddComponent<GraphicRaycaster>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        FadeCanvasObject.AddComponent<FadeManager>();

        //最前面になるよう適当なソートオーダー設定
        fadeCanvas.sortingOrder = 100;

        //フェード用のImage生成
        fadeImage = new GameObject("ImageFade").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform, false);
        fadeImage.rectTransform.anchoredPosition = Vector3.zero;
        fadeImage.raycastTarget = false;

        //Imageサイズは適当に大きく設定してください
        fadeImage.rectTransform.sizeDelta = new Vector2(9999, 9999);
    }
    // Update is called once per frame
    void Update()
    {
        if (isFadeOut)
        {
            //経過時間から透明度計算
            alpha += Time.unscaledDeltaTime / fadeTime;

            //フェードアウト終了判定
            if (alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;

                Quit();
            }

            //フェード用Imageの色・透明度設定
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }
    /// <summary>
    /// 同じシーンを開始
    /// </summary>
    public void ReStart()
    {
        FadeManager.FadeOut(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// フェードアウト開始
    /// </summary>
    public void FadeOut()
    {
        if (fadeImage == null) Init();
        fadeImage.color = Color.clear;
        fadeCanvas.enabled = true;
        isFadeOut = true;
    }
    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
    /// <summary>
    /// ステージセレクト画面へ移動
    /// </summary>
    public void StageSelect()
    {
        FadeManager.FadeOut(4);
    }
    /// <summary>
    /// タイトルへ移動
    /// </summary>
    public void Title()
    {
        FadeManager.FadeOut(1);
    }
}

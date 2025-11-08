using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

public class TitleScript : MonoBehaviour
{
    [SerializeField] UnityEvent Play;
    //　非同期動作で使用するAsyncOperation
    private AsyncOperation async;
    //　シーンロード中に表示するUI画面
    [SerializeField] private GameObject loadUI;

    bool loadingFade;
    private void Awake()
    {
        SEManager.Instance.Stop();
    }
    void Start()
    {
        Play.Invoke();
    }
    public void GameStart()
    {
        FadeManager.FadeOut(1);
    }
    /// <summary>
    /// ステージセレクトへ移動
    /// </summary>
    public void NextScene()
    {
        //　ロード画面UIをアクティブにする
        loadUI.SetActive(true);

        //　コルーチンを開始
        StartCoroutine("LoadData",1);
    }
    /// <summary>
    /// ロード画面表示
    /// </summary>
    /// <param name="n">移動先のシーン番号</param>
    /// <returns></returns>
    IEnumerator LoadData(int n)
    {
        yield return new WaitForSeconds(0.5f);
        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync(n);

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;
using UnityEngine.SceneManagement;
using Rewired;

public class LogoSceneScript : MonoBehaviour
{
    CanvasGroup canvasGroup;
    private bool logoStart;
    private bool logoEnd;
    private float fadeTime = 1.0f;
    private float fadeEndTime = 3;
    private AsyncOperation async;
    AudioSource audioSource;

    static public bool controller;
    // Start is called before the first frame update
    private void Awake()
    {
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;
        ReInput.ControllerPreDisconnectEvent += OnControllerPreDisconnect;

        BGMSwitcher.FadeOutAndFadeIn(SEPath.SOUNDLOGO30, 2, 1, 1, 0, 1, false);
        canvasGroup = GetComponent<CanvasGroup>();
        logoStart = true;
    }
    void OnControllerConnected(ControllerStatusChangedEventArgs args)
    {
        controller = true;
    }
    void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
    {
        controller = false;
    }
    void OnControllerPreDisconnect(ControllerStatusChangedEventArgs args)
    {
        controller = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SaveManager.saveClearStage(6);
        }
        if (!logoEnd)
        {
            if (Input.anyKeyDown)
            {
                logoEnd = true;
                logoStart = false;
                fadeEndTime = 2;
            }
        }
        if (logoEnd)
        {
            canvasGroup.alpha += Time.unscaledDeltaTime / (fadeTime * fadeEndTime);
            if (canvasGroup.alpha >= 1.0f)
            {
                logoEnd = false;
                canvasGroup.alpha = 1.0f;
                StartCoroutine("LoadData", 1);
            }
        }
        else if(logoStart)
        {
            canvasGroup.alpha -= 0.01f;
            if (canvasGroup.alpha <= 0.0f)
            {
                logoStart = false;
                canvasGroup.alpha = 0.0f;
                Invoke("LogoEndScene", 3.0f);
            }
        }
    }
    /// <summary>
    /// ロゴの表示終了
    /// </summary>
    void LogoEndScene()
    {
        logoEnd = true;
        BGMManager.Instance.FadeOut(SEPath.SOUNDLOGO30,2);
    }
    IEnumerator LoadData(int n)
    {
        yield return new WaitForSeconds(0.5f);

        // シーンの読み込みをする
        async = SceneManager.LoadSceneAsync(n);
    }
}

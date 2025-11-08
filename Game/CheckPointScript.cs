using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

/// <summary>
/// ステージにある3つの松明
/// </summary>
public class CheckPointScript : MonoBehaviour,ITorcable
{
    [SerializeField] private GameObject fire;       //松明の火
    [SerializeField] private Light light;           //ポイントライト
    [SerializeField] private Image fireUI;          //火のランクの画像
    private Light fireLight;
    private bool fireOn;
    private bool lightOn;
    private float alpha = 0;
    private float fadeTime = 2;
    private float lightIntensity = 10.0f;
    private GameObject playerCandle;
    Player player;
    AudioSource audio;
    bool clear;

    void Start()
    {
        playerCandle = GameObject.FindGameObjectWithTag("Player");
        player = playerCandle.GetComponent<Player>();
        fire = transform.GetChild(0).gameObject;
        light = light.GetComponent<Light>();
        fireLight = transform.GetChild(1).GetComponent<Light>();
        audio = transform.GetChild(1).GetComponent<AudioSource>();
    }
    void Update()
    {
        if(clear)
        {
            if (light.intensity < 1)
                light.intensity += 0.01f;
            if (light.intensity > 1)
            {
                light.intensity = 1;
                FadeManager.FadeOut(1);
            }
        }
        if(lightOn)
        {
            fireLight.intensity += Time.deltaTime * 5;
            if (fireLight.intensity >= lightIntensity)
            {
                fireLight.intensity = lightIntensity;
                lightOn = false;
            }
        }
        if(fireOn)
        {
            //経過時間から透明度計算
            alpha += Time.unscaledDeltaTime / fadeTime;         //2秒で表示

            //フェードアウト終了判定
            if (alpha >= 1.0f)
            {
                fireOn = false;
                alpha = 1.0f;
            }

            //フェード用Imageの色・透明度設定
            fireUI.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }
    }
    public void TorchFire()
    {
        GetComponent<BoxCollider>().enabled = false;
        fire.SetActive(true);
        SEManager.Instance.Play(SEPath.IGNITION, 1, 0, 1, false, () =>
        {
            audio.Play();
        });
        player.ClearFire(fire.name);
        fireOn = true;
        lightOn = true;
    }
}

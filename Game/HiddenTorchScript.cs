using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
using UnityEngine.Events;

/// <summary>
/// 隠し松明
/// </summary>
public class HiddenTorchScript : MonoBehaviour, ITorcable
{
    private ParticleSystem fireParticle;        //火のエフェクト
    private Light fireLight;                    //ポイントライト
    private AudioSource audio;
    private bool lightOn;
    static public bool hidden = false;      //隠し要素
    // Start is called before the first frame update
    void Start()
    {
        hidden = false;
        fireParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        fireLight = transform.GetChild(1).GetComponent<Light>();
        audio = transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightOn)
        {
            fireLight.intensity += Time.deltaTime * 5;

            if (fireLight.intensity >= 10)
            {
                fireLight.intensity = 10;
                lightOn = false;
            }
        }
    }
    /// <summary>
    /// 燃えた時
    /// </summary>
    public void TorchFire()
    {
        hidden = true;
        GetComponent<BoxCollider>().enabled = false;
        fireParticle.Play();
        SEManager.Instance.Play(SEPath.IGNITION, 1, 0, 1, false, () =>
        { 
            audio.Play();
        });
        lightOn = true;
    }
}

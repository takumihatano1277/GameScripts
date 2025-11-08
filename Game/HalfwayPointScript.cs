using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
using UnityEngine.Events;

/// <summary>
/// 中間地点を設定
/// </summary>
public class HalfwayPointScript : MonoBehaviour, ITorcable
{
    private ParticleSystem fireParticle;        //セーブポイントの火
    private Light fireLight;                    //ポイントライト
    private AudioSource audio;
    private bool lightOn;
    private float lightIntensity = 10.0f;
    [SerializeField] private UnityEvent TrapEvent;
    void Start()
    {
        fireParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        fireLight = transform.GetChild(1).GetComponent<Light>();
        audio = transform.GetChild(1).GetComponent<AudioSource>();
        if(GameManagerScript.IsHalfSave())
        {
            TorchFire();
            TrapEvent.Invoke();
        }
    }
    void Update()
    {
        if (lightOn)
        {
            fireLight.intensity += Time.deltaTime * 5;

            if (fireLight.intensity >= lightIntensity)
            {
                fireLight.intensity = lightIntensity;
                lightOn = false;
            }
        }
    }
    public void TorchFire()
    {
        SEManager.Instance.Play(SEPath.SAVE, 1);
        GetComponent<MeshCollider>().enabled = false;
        fireParticle.Play();
        SEManager.Instance.Play(SEPath.IGNITION, 1, 0, 1, false, () =>
        {
            audio.Play();
        });
        GameManagerScript.SavePoint(true);
        lightOn = true;
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;
using KanKikuchi.AudioManager;

public class SetVolumeScript : MonoBehaviour
{
    [SerializeField] private UnityEvent LoadData;
    private Slider slider;
    [SerializeField] private GameObject[] SEObj;
    [SerializeField] private List<AudioSource> audioSources;
    private void Start()
    {
        //SaveManager.load();
        SEObj = GameObject.FindGameObjectsWithTag("SE");
        for (int i = 0; i < SEObj.Length; i++)
        {
            audioSources.Add(SEObj[i].GetComponent<AudioSource>());
        }
        slider =GetComponent<Slider>();
        LoadData.Invoke();

        switch(transform.parent.name)
        {
            case "Master":
                MasterVolumeReset();
                break;
            case "BGM":
                BGMVolumeReset();
                break;
            case "SE":
                SEVolumeReset();
                break;
        }

        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i] == null)
            {
                return;
            }
            audioSources[i].volume = SaveManager.sd.se * SaveManager.sd.master;
        }
    }
    /// <summary>
    /// マスターボリューム保存
    /// </summary>
    public float MasterVolume
    {
        set
        {
            SaveManager.SaveMaster(slider.value);
            SEManager.Instance.ChangeBaseVolume(SaveManager.sd.master*SaveManager.sd.se);
            BGMManager.Instance.ChangeBaseVolume(SaveManager.sd.master * SaveManager.sd.bgm);
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (audioSources[i] == null)
                {
                    return;
                }
                audioSources[i].volume = SaveManager.sd.se*SaveManager.sd.master;
            }
        }
    }
    /// <summary>
    /// BGMボリューム保存
    /// </summary>
    public float BGMVolume
    {
        set
        {
            SaveManager.SaveBGM(slider.value);
            BGMManager.Instance.ChangeBaseVolume(SaveManager.sd.bgm*SaveManager.sd.master);
        }

    }
    /// <summary>
    /// SEボリューム保存
    /// </summary>
    public float SEVolume
    {
        set
        {
            SaveManager.SaveSE(slider.value);
            SEManager.Instance.ChangeBaseVolume(SaveManager.sd.se * SaveManager.sd.master);
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (audioSources[i] == null)
                {
                    return;
                }
                audioSources[i].volume = SaveManager.sd.se * SaveManager.sd.master;
            }
        }
    }
    /// <summary>
    /// マスターボリューム初期化
    /// </summary>
    public void MasterVolumeReset()
    {
        SaveManager.SaveMaster(slider.value);
        SEManager.Instance.ChangeBaseVolume(SaveManager.sd.master * SaveManager.sd.se);
        BGMManager.Instance.ChangeBaseVolume(SaveManager.sd.master * SaveManager.sd.bgm);
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i] == null)
            {
                return;
            }
            audioSources[i].volume = SaveManager.sd.se * SaveManager.sd.master;
        }
    }
    /// <summary>
    /// BGMボリューム初期化
    /// </summary>
    public void BGMVolumeReset()
    {
        SaveManager.SaveBGM(slider.value);
        BGMManager.Instance.ChangeBaseVolume(SaveManager.sd.bgm * SaveManager.sd.master);
    }
    /// <summary>
    /// SEボリューム初期化
    /// </summary>
    public void SEVolumeReset()
    {
        SaveManager.SaveSE(slider.value);
        SEManager.Instance.ChangeBaseVolume(SaveManager.sd.se * SaveManager.sd.master);
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (audioSources[i] == null)
            {
                return;
            }
            audioSources[i].volume = SaveManager.sd.se * SaveManager.sd.master;
        }
    }
    /// <summary>
    /// マスターボリューム読み込み
    /// </summary>
    public void LoadMasterVolume()
    {
        slider.value = SaveManager.sd.master;
    }
    /// <summary>
    /// BGMボリューム読み込み
    /// </summary>
    public void LoadBGMVolume()
    {
        slider.value = SaveManager.sd.bgm;
    }
    /// <summary>
    /// SEボリューム読み込み
    /// </summary>
    public void LoadSEVolume()
    {
        slider.value = SaveManager.sd.se;
    }
}
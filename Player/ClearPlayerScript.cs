using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

public class ClearPlayerScript : MonoBehaviour
{
    private Player player;
    [SerializeField] private ParticleSystem clearEffect;        //クリア時に出てくるエフェクト
    void Init()
    {
        player = GetComponent<Player>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lamp")
        {
            ClearStage();
            player.ClearFlg();
        }
    }
    public void ClearStage()
    {
        SEManager.Instance.Play(SEPath.CLEAR, 1);       //クリアSE

        GameManagerScript.CameraStop(true);
        GameManagerScript.WallStop(true);
        if (SceneManager.GetActiveScene().buildIndex >= 9)
        {
            SaveManager.saveStageHidden(SceneManager.GetActiveScene().buildIndex - 8, HiddenTorchScript.hidden);    //隠し要素の進行度を保存
        }
        SaveManager.saveClearStage(GameManagerScript.stageNo);        //現在の進行度を保存
        clearEffect.Play();
    }
}

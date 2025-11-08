using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;
using System.Linq;

/// <summary>
/// 燃えるオブジェクト
/// </summary>
public class FireScript : MonoBehaviour,IIgnitioable
{
    [SerializeField] private ParticleSystem[] fire;         //火のエフェクト
    [SerializeField] private GameObject[] fall;             //落下するものがあったら入れる
    [SerializeField] private GameObject[] subRope;          //他に燃えるロープがあったら入れる
    [SerializeField] private GameObject[] barrel;           //樽を入れる
    private HighlightEffect highlight;
    BreakObjectScript breakObjectScript;
    [SerializeField] private GameObject wood;               //木の板
    [SerializeField] private bool barrelObj = false;
    Rigidbody rigidbodies;
    BoxCollider boxCollider;
    [SerializeField] private AudioSource[] audio;
    // Start is called before the first frame update
    void Start()
    {
        highlight = GetComponent<HighlightEffect>();
    }
    /// <summary>
    /// オブジェクトが燃えたら
    /// </summary>
    public void FireOn()
    {
        if(!barrelObj)
        {
            if (highlight != null)          //ハイライトオンになっていたら
            {
                highlight.highlighted = false;
            }
            GetComponent<BoxCollider>().enabled = false;
            audio[0].Play();
            for (int i = 0; i < fire.Length; i++)
            {
                fire[i].Play();
            }
            Invoke("FallObj", 3.0f);
            Invoke("FireDestroy", 5.0f);
        }
        if(barrelObj)
        {
            audio[0].Play();
            for (int i = 0; i < fire.Length; i++)
            {
                fire[i].Play();
            }
            for (int i = 0; i < barrel.Length; i++)
            {
                if (barrel[i] != null)
                {
                    Invoke("destroyObject",1.5f);
                }
            }
        }
    }
    /// <summary>
    /// 燃えた時に落下する
    /// </summary>
    void FallObj()
    {
        if(wood!=null)
        {
            breakObjectScript = wood.GetComponent<BreakObjectScript>();
            breakObjectScript.KinematicOff();
        }
        for (int u = 0; u < fall.Length; u++)
        {
            rigidbodies = fall[u].GetComponent<Rigidbody>();
            rigidbodies.isKinematic = false;
        }
        for (int i = 1; i < audio.Length; i++)
        {
            audio[i].Play();
        }
    }
    /// <summary>
    /// 燃えたロープを削除
    /// </summary>
    void FireDestroy()
    {
        for(int i=0;i<subRope.Length;i++)
        {
            if (subRope[i] != null)
            {
                Destroy(subRope[i]);
            }
        }
        Destroy(gameObject);
    }
    /// <summary>
    /// 樽が燃えた場合飛び散る
    /// </summary>
    public void destroyObject()
    {
        GetComponent<BoxCollider>().enabled = false;
        var random = new System.Random();
        var min = -30;      //かかる最小力
        var max = 30;       //かかる最大力
        for (int i = 0; i < barrel.Length; i++)
        {
            if (barrel[i] != null)
            {
                barrel[i].GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
                    r.isKinematic = false;
                    r.transform.SetParent(null);
                    r.gameObject.AddComponent<AutoDestroy>().time = 2f;
                    var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
                    r.AddForce(vect, ForceMode.Impulse);
                    r.AddTorque(vect, ForceMode.Impulse);
                });
                Destroy(barrel[i]);
            }
        }
    }
}

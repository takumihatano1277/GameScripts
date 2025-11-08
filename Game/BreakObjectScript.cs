using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KanKikuchi.AudioManager;

/// <summary>
/// 飛び散らせて破壊
/// </summary>
public class BreakObjectScript : MonoBehaviour, ITrapable
{
    bool first = false;
    AudioSource audioSource;
    Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void TrapAttack()
    {
        destroyObject();
    }
    public void destroyObject()
    {
        SEManager.Instance.Play(SEPath.DEMOLISH_WOOD_METAL_STEREO, 1);      //飛び散る際のSE
        var random = new System.Random();
        var min = -3;       //最小力
        var max = 3;        //最大力
        gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
            r.isKinematic = false;
            r.transform.SetParent(null);
            r.gameObject.AddComponent<AutoDestroy>().time = 2f;     //オートデストロイスクリプトを割り当て2秒後に削除
            var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));  //力のかけ具合
            r.AddForce(vect, ForceMode.Impulse);
            r.AddTorque(vect, ForceMode.Impulse);
        });
        Destroy(gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        if(!first)
        {
            if (other.gameObject.tag == "Road")
            {
                first=true;
                audioSource.Play();
                gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
                    r.isKinematic = true;
                });         //子オブジェクトを全て固定
                gameObject.GetComponentsInChildren<MeshCollider>().ToList().ForEach(m => {
                    m.isTrigger = false;
                });         //子オブジェクトのColliderを全てオン
            }
        }
    }
    public void KinematicOff()
    {
        gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
            r.isKinematic = false;
        });                 //子オブジェクトを全て固定解除
        gameObject.GetComponentsInChildren<MeshCollider>().ToList().ForEach(m => {
            m.isTrigger = true;
        });                 //子オブジェクトのColliderを全てオフ
    }
}

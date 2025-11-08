using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterScript : MonoBehaviour,IWateable
{
    [SerializeField] private ParticleSystem[] explosion;
    [SerializeField] private GameObject[] barrel;
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private GameObject wall;       //進行不可能の壁
    private bool first = false;         //初めて蝋が当たったかどうか
    private void OnTriggerStay(Collider other)
    {
        var water = other.gameObject.GetComponent<IWateable>();
        if (water != null)
        {
            water.WaterHit();
        }
    }

    public void WaterHit()
    {
        
    }
    /// <summary>
    /// 水に蝋が当たった時
    /// </summary>
    public void WaterExplosion()
    {
        if(!first)
        {
            first = true;
            if(wall!=null)
            {
                wall.SetActive(false);
            }
            for (int i = 0; i < barrel.Length; i++)
            {
                if(barrel[i] != null)
                {
                    destroyObject(barrel[i]);
                }
            }
            for (int i = 0; i < explosion.Length; i++)
            {
                if (explosion[i] != null)
                {
                    explosion[i].Play();
                }
                if (audioSources[i] != null)
                {
                    audioSources[i].Play();
                }
            }
        }
    }
    /// <summary>
    /// 爆発した際に飛び散るように力を加える
    /// </summary>
    /// <param name="obj">飛び散るオブジェクト</param>
    public void destroyObject(GameObject obj)
    {
        var random = new System.Random();
        var min = -30;
        var max = 30;
        obj.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
            r.isKinematic = false;
            r.transform.SetParent(null);
            r.gameObject.AddComponent<AutoDestroy>().time = 2f;
            var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
            r.AddForce(vect, ForceMode.Impulse);
            r.AddTorque(vect, ForceMode.Impulse);
        });
        Destroy(obj);
    }
}

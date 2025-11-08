using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

/// <summary>
/// 氷が溶けるように
/// </summary>
public class IceBlockScript : MonoBehaviour
{
    [SerializeField] private float meltAmount = 0.03f;
    private GameObject iceScale;
    [SerializeField] private bool melt;
    private Player player;
    private BoxCollider boxCollider;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boxCollider = GetComponent<BoxCollider>();
        iceScale = transform.parent.gameObject;
    }
    void Update()
    {
        if(Player.isFireHeat && melt)
        {
            MeltIce();
        }
    }
    /// <summary>
    /// 氷が溶ける
    /// </summary>
    public void MeltIce()
    {
        iceScale.transform.localScale -= new Vector3(0.0f, meltAmount, 0.0f);
        boxCollider.isTrigger = true;
        if (iceScale.transform.localScale.y<=0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Heat" && Player.isFireHeat && !melt)
        {
            melt = true;
            SEManager.Instance.Play(SEPath.ICE, 1, 0, 1, true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Player.isFireHeat)
        {
            boxCollider.isTrigger = false;
            melt = false;
            SEManager.Instance.Stop(SEPath.ICE);
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player"&&!Player.isFireHeat)
        {
            boxCollider.isTrigger = false;
            melt = false;
            SEManager.Instance.Stop(SEPath.ICE);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Heat")
        {
            boxCollider.isTrigger = false;
            melt = false;
            SEManager.Instance.Stop(SEPath.ICE);
        }
        if (other.gameObject.tag == "Player")
        {
            boxCollider.isTrigger = false;
            melt = false;
            SEManager.Instance.Stop(SEPath.ICE);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

/// <summary>
/// 回復スポット　healPointで回復量指定
/// </summary>
public class HealScript : MonoBehaviour
{
    Player player;
    SphereCollider sphereCollider;
    [SerializeField] private int healPoint = 60;        //回復量
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = true;
        foreach (Transform particle in transform)
        {
            particle.GetComponent<ParticleSystem>().loop = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.Damage(-healPoint);
            sphereCollider.enabled = false;
            SEManager.Instance.Play(SEPath.CHARGE_SERVO_24_SEMI_UP_1000MS_STEREO, 1);       //回復SE
            foreach (Transform particle in transform)
            {
                particle.GetComponent<ParticleSystem>().loop = false;
            }
        }
    }
}

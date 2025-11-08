using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject obj;
    private AudioSource SE;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        obj = gameObject.transform.GetChild(0).gameObject;
        SE = obj.GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        var TrapTarget = collision.gameObject.GetComponent<ITrapable>();
        if (TrapTarget != null)
        {
            audioSource.Play();
            SE.Play();
            TrapTarget.TrapAttack();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var TrapTarget = other.gameObject.GetComponent<ITrapable>();
        if (TrapTarget != null)
        {
            audioSource.Play();
            SE.Play();
            TrapTarget.TrapAttack();
        }
    }
}

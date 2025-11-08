using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        var TrapTarget = collision.gameObject.GetComponent<ITrapable>();
        if (TrapTarget != null)
        {
            TrapTarget.TrapAttack();
        }
    }
    public void TrapSE()
    {
        audioSource.Play();
    }
}

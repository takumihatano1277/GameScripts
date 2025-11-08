using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHealScript : MonoBehaviour
{
    [SerializeField] private GameObject healFire;
    private void OnParticleSystemStopped()
    {
        Instantiate(healFire);      //チュートリアルでは回復の火が使った後も生成されるように
        Destroy(transform.parent.gameObject);
    }
}

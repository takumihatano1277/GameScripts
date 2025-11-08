using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SEManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        FadeManager.FadeIn();
    }
}

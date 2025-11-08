using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パーティクル終了時親削除
/// </summary>
public class DestroyScript : MonoBehaviour
{
    private bool digestion;
    [SerializeField] Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = light.GetComponent<Light>();
        Invoke("LightOff", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (digestion)
            light.range -= 0.08f;
    }
    private void OnParticleSystemStopped()
    {
        Destroy(transform.parent.gameObject);
    }
    void LightOff()
    {
        digestion = true;
    }
}

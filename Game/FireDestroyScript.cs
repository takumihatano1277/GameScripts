using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火のエフェクト終了時親削除
/// </summary>
public class FireDestroyScript : MonoBehaviour
{
    /// <summary>
    /// パーティクル終了時親削除
    /// </summary>
    private void OnParticleSystemStopped()
    {
        Destroy(transform.parent.gameObject);
    }
}

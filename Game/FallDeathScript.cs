using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 落下したら死亡するように
/// </summary>
public class FallDeathScript : MonoBehaviour
{
    /// <summary>
    /// 落下死亡
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        var TrapTarget = other.gameObject.GetComponent<ITrapable>();
        if (TrapTarget != null)
        {
            TrapTarget.TrapAttack();
        }
    }
}

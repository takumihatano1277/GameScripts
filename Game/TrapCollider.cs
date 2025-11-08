using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var TrapTarget = collision.gameObject.GetComponent<ITrapable>();
        if (TrapTarget != null)
        {
            TrapTarget.TrapAttack();
        }
    }
}

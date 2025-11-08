using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHeadScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(Player.isFireMotion)
        {
            var fireTarget = other.gameObject.GetComponent<IIgnitioable>();     //点火モーションをした相手が燃えるものなら
            if (fireTarget != null)
            {
                fireTarget.FireOn();
            }
        }
        if (Player.isFireMotion)
        {
            var torch = other.gameObject.GetComponent<ITorcable>();             //点火モーションをした相手が松明なら
            if (torch != null)
            {
                torch.TorchFire();
            }
        }
    }
}

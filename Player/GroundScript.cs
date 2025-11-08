using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private static bool isGround;
    //地面に触れていないとジャンプができない、氷では条件付き
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SE")
        {
            Ground(true);
        }
        else if (other.gameObject.tag == "Road")
        {
            Ground(true);
        }
        else if (other.gameObject.tag == "UpRoad")
        {
            Ground(true);
        }
        else if (other.gameObject.tag == "Box")
        {
            Ground(true);
        }
        else if (other.gameObject.tag == "Ice" && !Player.isFireHeat && !other.GetComponent<BoxCollider>().isTrigger)
        {
            Ground(true);
        }
    }
    //地面から離れている
    private void OnTriggerExit(Collider other)
    {
        Ground(false);
    }
    public static void Ground(bool _ground)
    {
        isGround = _ground;
    }
    public static bool IsGround()
    {
        return isGround;
    }
}

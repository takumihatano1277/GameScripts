using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrapScript : MonoBehaviour
{
    [SerializeField] Animation[] animation;
    [SerializeField] float anim_y;

    private void Awake()
    {
        anim_y = transform.position.y - 2.5f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Box")
        {
            for (int i = 0; i < animation.Length; i++)
            {
                animation[i].enabled = false;
                animation[i].gameObject.transform.position = new Vector3(animation[i].gameObject.transform.position.x, anim_y, animation[i].gameObject.transform.position.z);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Box")
        {
            Invoke("AnimStart", 1.0f);

        }
    }
    /// <summary>
    /// アニメーション開始
    /// </summary>
    public void AnimStart()
    {
        for (int i = 0; i < animation.Length; i++)
        {
            animation[i].enabled = true;
        }
    }
}

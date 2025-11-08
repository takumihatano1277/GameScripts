using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorScaleScript : MonoBehaviour
{
    private Animator animator;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;      //時間が止まっていてもアニメーションはするように
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

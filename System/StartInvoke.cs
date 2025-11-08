using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartInvoke : MonoBehaviour
{
    [SerializeField] private UnityEvent StartEvent;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnEvent", 2.0f);
        
    }
    public void OnEvent()
    {
        StartEvent.Invoke();
    }
}

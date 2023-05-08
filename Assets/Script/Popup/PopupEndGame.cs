using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupEndGame : MonoBehaviour
{
    public event Action Click;
    public void ClickYes()
    {
        Click?.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

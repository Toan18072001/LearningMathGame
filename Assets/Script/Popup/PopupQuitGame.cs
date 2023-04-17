using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupQuitGame : MonoBehaviour
{
    public event Action Click;
    public void ClickYes()
    {
        Click?.Invoke();
    }

    public void ClickNo()
    {
       this.transform.gameObject.SetActive(false);
    }
}

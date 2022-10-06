using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager
{
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (Input.anyKey == false)  // 어떤 키던 간에 false 면 그냥 return
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }
    
    public void Clear()
    {
        KeyAction = null;
    }
}

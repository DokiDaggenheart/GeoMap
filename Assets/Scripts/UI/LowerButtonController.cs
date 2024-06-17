using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerButtonController
{
    public static event Action TurnOffButtons;
    public static void InvokeTurnOffButtons()
    {
        TurnOffButtons?.Invoke();
    }
}

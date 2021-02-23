using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvent
{

    // delegate (cant becalled outside of the class)
    public delegate void MouseCursorEnable(bool Enabled);

    // event
    public static event MouseCursorEnable CursorEnabled;



    // public method for invoking the deligate
    public static void InvokeMouseCursorEnable(bool enabled)
    {
        // if variable enabled, invoke event
        CursorEnabled?.Invoke(enabled);
    }


}

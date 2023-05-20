using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicJoystickExtended : DynamicJoystick
{
    public void ResetDirection()
    {
        base.HandleInput(0, Vector2.one, Vector2.one,null);
    }
}

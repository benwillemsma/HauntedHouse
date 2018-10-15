using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTrigger
{
    private enum AxisState
    {
        Idle = -1,
        UP = 0,
        DOWN = 1,
        STAY = 2,
    }

    private string axisName;
    private AxisState state = AxisState.Idle;

    public InputTrigger(string axisName)
    {
        this.axisName = axisName;
    }

    public bool Down
    {
        get
        {
            Update();
            return state == AxisState.DOWN;
        }
    }
    public bool Stay
    {
        get
        {
            Update();
            return state == AxisState.STAY;
        }
    }
    public bool Up
    {
        get
        {
            Update();
            return state == AxisState.UP;
        }
    }

    public float value
    {
        get { return Input.GetAxis(axisName); }
    }
    public int clampedValue
    {
        get
        {
            return Input.GetAxisRaw(axisName) != 0 ? 
                Input.GetAxisRaw(axisName) > 0 ? 
                1 : -1 : 0;
        }
    }

    private bool pressed = false;
    private void Update()
    {
        if (!pressed && Input.GetAxisRaw(axisName) > 0)
        {
            state = AxisState.DOWN;
            pressed = true;
        }
        else if (pressed && Input.GetAxisRaw(axisName) > 0)
            state = AxisState.STAY;

        else if (pressed && Input.GetAxisRaw(axisName) <= 0)
        {
            state = AxisState.UP;
            pressed = false;
        }
        else if (!pressed && Input.GetAxisRaw(axisName) <= 0)
            state = AxisState.Idle;
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    private InputTrigger rightTriggerState = new InputTrigger("RightTrigger");
    public InputTrigger RightTrigger
    {
        get { return rightTriggerState; }
    }

    private InputTrigger leftTriggerState = new InputTrigger("LeftTrigger");
    public InputTrigger LeftTrigger
    {
        get { return leftTriggerState; }
    }
}
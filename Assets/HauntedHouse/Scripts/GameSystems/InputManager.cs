using System;
using System.Collections;
using UnityEngine;
using Valve.VR;

public class InputManager : MonoBehaviour
{
    public float moveThreshold = 1;

    [Space(10)]
    public SteamVR_Behaviour_Pose rightHand;
    public SteamVR_Behaviour_Pose leftHand;
    
    public bool IsMoving
    {
        get
        {
            float rightSpeed = rightHand.GetAngularVelocity().x;
            float leftSpeed = leftHand.GetAngularVelocity().x;

            if (Mathf.Abs(rightSpeed) > moveThreshold && Mathf.Abs(leftSpeed) > moveThreshold)
            {
                if ((rightSpeed > 0 && leftSpeed < 0) || (rightSpeed < 0 && leftSpeed > 0))
                    return true;
            }
            return false;
        }
    }

    private void Update()
    {
        //Debug.Log(IsMoving);
    }
}
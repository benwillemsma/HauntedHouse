using System;
using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputManager : MonoBehaviour
{
    public float moveThreshold = 1;

    [Space(10)]
    public SteamVR_Behaviour_Pose rightHand;
    public SteamVR_Behaviour_Pose leftHand;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

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

    public bool IsCrawling
    {
        get { return Player.instance.eyeHeight < PlayerData.Instance.crawlThreshold; }
    }

    private void Update()
    {
        if(IsCrawling && body)
        {
            if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
                body.velocity = Vector3.ProjectOnPlane(leftHand.GetVelocity(), Vector3.up);

            if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
                body.velocity = Vector3.ProjectOnPlane(rightHand.GetVelocity(), Vector3.up);
        }
    }
}
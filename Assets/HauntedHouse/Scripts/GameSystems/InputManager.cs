using System;
using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputManager : MonoBehaviour
{
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

            float sensitivity = PlayerData.Instance.moveSensitivity;
            if (Mathf.Abs(rightSpeed) > sensitivity && Mathf.Abs(leftSpeed) > sensitivity)
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
        if (body)
        {
            if (IsCrawling)
            {
                if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
                {
                    // turn off left controller movement
                    body.velocity = Vector3.ProjectOnPlane(leftHand.GetVelocity(), Vector3.up);
                }
                else
                {
                    // turn on left controller movement
                }

                if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    // turn off right controller movement
                    body.velocity = Vector3.ProjectOnPlane(rightHand.GetVelocity(), Vector3.up);
                }
                else
                {
                    // turn on right controller movement
                }
            }
            else if (IsMoving)
            {
                Vector3 handForward = Vector3.Cross((leftHand.transform.position - rightHand.transform.position), Vector3.up);
                body.velocity = Vector3.Lerp(body.velocity, handForward, Time.deltaTime);
            }
        }
    }
}
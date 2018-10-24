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

    public Hand noSteamVRHand;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public bool IsCrawling
    {
        get { return Player.instance.eyeHeight < PlayerData.Instance.crawlThreshold; }
    }

    private void Update()
    {
        if (noSteamVRHand && noSteamVRHand.enabled == false) noSteamVRHand.enabled = true;
        if (body) 
        {
            Vector2 moveDir = Vector3.zero;//SteamVR_Input.de(SteamVR_Input_Sources.Any);
            if (moveDir.magnitude > 0)
                body.velocity = new Vector3(moveDir.x, body.velocity.y, moveDir.y).normalized * PlayerData.Instance.moveSpeed;

            if (IsCrawling)
                body.velocity *= 0.5f;
        }
    }
}
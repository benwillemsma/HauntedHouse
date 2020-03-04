using System;
using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class InputManager : MonoBehaviour
{
    public Transform head;
    public Rigidbody body;
    public float moveSpeed = 1.2f;

    private void Update()
    {
        if (body && head)
        {
            Vector3 moveDir = SteamVR_Input.__actions_default_in_Move.GetAxis(SteamVR_Input_Sources.Any);
            Debug.Log("moveDirection:" + moveDir);
            moveDir = Quaternion.AngleAxis(head.eulerAngles.y, Vector3.up) * new Vector3(moveDir.x, 0, moveDir.y);
            body.velocity = (moveDir * moveSpeed) + (Vector3.up * body.velocity.y);
            Debug.Log("Velocity:" + moveDir);
        }
    }
}
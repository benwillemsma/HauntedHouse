using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool inverted;

    private void LateUpdate()
    {
        float multiplier = Time.deltaTime * PlayerData.Instance.CameraSensitivity * (inverted ? 1 : -1);
        transform.Rotate(transform.right, Input.GetAxis("Mouse Y") * multiplier, Space.World);
    }
}

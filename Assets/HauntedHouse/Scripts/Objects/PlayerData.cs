using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    
    [Range(0, 10)]
    public float moveSpeed = 8;
    
    [Range(0, 50)]
    public float CameraSensitivity = 30;
    [Range(0, 2)]
    public float CameraOffset = 0.75f;
    public bool InvertedCamera;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }
}

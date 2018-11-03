using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [Range(0, 10)]
    public float moveSpeed = 1.2f;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }
}

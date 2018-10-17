using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    [Header("Movement Settings")]
    [Range(0, 10)]
    public float moveSpeed = 3;
    [Range(0, 3), Tooltip("Sensitivity of Controller for shake movement")]
    public float moveSensitivity = 1;

    [Header("Crawl Settings")]
    [Range(0, 1)]
    public float crawlHeight = 0.2f;
    [Range(0, 2), Tooltip("Height threshold where the player is considered crawling")]
    public float crawlThreshold = 1;

    [Header("Camera Settings")]
    [Range(0, 50)]
    public float CameraSensitivity = 30;
    public bool InvertedCamera;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);
    }
}

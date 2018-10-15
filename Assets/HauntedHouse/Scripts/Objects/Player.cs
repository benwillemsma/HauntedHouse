using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [SerializeField, Range(0, 10)]
    private float moveSpeed = 8;

    [Space(10)]
    public Transform camera;
    [Range(0, 50)]
    public float CameraSensitivity = 30;
    [Range(0, 2)]
    public float CameraOffset = 0.75f;
    public bool InvertedCamera;

    private InputController ic;
    private Rigidbody rb;
    private Animator anim;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else Destroy(gameObject);

        ic = GetComponent<InputController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = rb.velocity.y;
        rb.velocity = transform.rotation * moveDirection;
        rb.AddForce(Vector3.down * 15f);
    }

    private void LateUpdate()
    {
        transform.Rotate(transform.up, Input.GetAxis("Mouse X") * Time.deltaTime * CameraSensitivity, Space.World);
        float multiplier = Input.GetAxis("Mouse Y") * Time.deltaTime * CameraSensitivity * (InvertedCamera ? 1 : -1);
        camera.Rotate(transform.right, multiplier, Space.World);
    }
}

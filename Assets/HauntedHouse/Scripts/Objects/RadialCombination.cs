using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class RadialCombination : MonoBehaviour
{
    public enum RotationDirection
    {
        ClockWise = 1,
        CounterCW = -1,
    }

    private enum Axis
    {
        x, y, z
    }

    [SerializeField]
    private UnityEvent openEvent;
    [SerializeField]
    private Transform attachPoint;
    [SerializeField]
    private Axis axis;

    [Space(10)]
    [SerializeField, Range(-5, 5)]
    private int[] Pattern;
    [SerializeField]
    private List<Collider> rotationTriggers = new List<Collider>();

    [SerializeField]
    private bool detach; // for testing

    [Header("Audio")]
    [SerializeField]
    private AudioClip RotationSound;
    [SerializeField]
    private float soundStep = 10;

    private Rigidbody rb;
    private AudioSource audioSource;
    public RotationDirection patternDir;
    private RotationDirection rotateDir;

    public int patternIndex = 0;
    public int triggerIndex = 1;
    public int rotations = 0;
    public int checkpoint = 1;

    private float prevRotation;

    public bool Attached
    {
        get { return transform.parent == attachPoint; }
        set
        {
            if (value)
            {
                rb.isKinematic = true;

                transform.parent = attachPoint;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
            else
            {
                rb.isKinematic = false;
                transform.parent = null;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        patternDir = (Pattern[patternIndex] > 0) ? RotationDirection.ClockWise : RotationDirection.CounterCW;
    }

    private void Update()
    {
        if (detach)
        {
            detach = false;
            Attached = false;
        }
        float axisRot = GetAxisRotation();
        if (prevRotation - axisRot > soundStep)
        {
            prevRotation = axisRot;
            audioSource.PlayOneShot(RotationSound);
        }
    }

    private float GetAxisRotation()
    {
        switch (axis)
        {
            case Axis.x:
                return transform.localEulerAngles.x;
            case Axis.y:
                return transform.localEulerAngles.y;
            case Axis.z:
                return transform.localEulerAngles.z;
        }
        return 0;
    }

    private void CheckPattern()
    {
        if (rotations / 4 == Pattern[patternIndex])
        {
            patternIndex++;
            if (patternIndex == Pattern.Length)
            {
                openEvent.Invoke();
                return;
            }
            patternDir = (RotationDirection)((int)patternDir * -1);
        }
        else if (rotateDir != patternDir)
        {
            rotations = 0;
            patternIndex = 0;
            patternDir = (RotationDirection)((int)patternDir * -1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Attached && patternIndex < Pattern.Length)
        {
            if (rotationTriggers.Contains(other))
            {
                int otherIndex = (triggerIndex < rotationTriggers.Count / 2 && triggerIndex >= 0) ? rotationTriggers.IndexOf(other) : rotationTriggers.LastIndexOf(other);
                if (otherIndex != triggerIndex)
                {
                    rotateDir = (otherIndex - triggerIndex > 0) ? RotationDirection.ClockWise : RotationDirection.CounterCW;
                    if(patternIndex > 0 || (patternIndex == 0 && (rotateDir == patternDir || rotations != 0)))
                        rotations += (int)rotateDir;
                    else
                    {
                        checkpoint--;
                        if (checkpoint < 1)
                            checkpoint += rotationTriggers.Count - 2;
                    }

                    triggerIndex += (int)rotateDir;
                    if (triggerIndex > rotationTriggers.Count - 2) triggerIndex = 1;
                    if (triggerIndex < 1) triggerIndex += rotationTriggers.Count - 2;
                    if (triggerIndex == checkpoint && rotations != 0) CheckPattern();
                }
            }
        }
        else if(!Attached && other.transform == attachPoint)
            Attached = true;
    }
}

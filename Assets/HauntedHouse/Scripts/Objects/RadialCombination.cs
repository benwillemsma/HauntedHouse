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
    private Axis axis;

    [Space(10)]
    [SerializeField]
    private int[] Pattern;
    [SerializeField]
    private List<Collider> rotationTriggers = new List<Collider>();
    [SerializeField]
    private int triggerAmount = 1;
    [SerializeField]
    private bool fullRotations = false;

    [Header("Audio")]
    [SerializeField]
    private AudioClip RotationSound;
    [SerializeField]
    private AudioClip ClickSound;
    [SerializeField]
    private float soundStep = 10;

    protected Rigidbody rb;
    protected AudioSource audioSource;
    public RotationDirection patternDir;
    private RotationDirection rotateDir;

    private int patternIndex = 0;
    private int triggerIndex = 1;
    private float rotations = 0;

    private float prevRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        patternDir = (Pattern[patternIndex] > 0) ? RotationDirection.ClockWise : RotationDirection.CounterCW;
    }

    protected virtual void Update()
    {
        float axisRot = GetAxisRotation();
        if (prevRotation - axisRot > soundStep)
        {
            prevRotation = axisRot;
            if (RotationSound) audioSource.PlayOneShot(RotationSound);
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (patternIndex < Pattern.Length)
        {
            if (rotationTriggers.Contains(other))
            {
                int otherIndex = (triggerIndex < rotationTriggers.Count / 2 && triggerIndex >= 0) ? rotationTriggers.IndexOf(other) : rotationTriggers.LastIndexOf(other);
                if (otherIndex != triggerIndex)
                {
                    rotateDir = (otherIndex - triggerIndex > 0) ? RotationDirection.ClockWise : RotationDirection.CounterCW;

                    triggerIndex += (int)rotateDir;
                    if (triggerIndex > rotationTriggers.Count - 2) triggerIndex = 1;
                    if (triggerIndex < 1) triggerIndex += rotationTriggers.Count - 2;

                    if (fullRotations)
                    {
                        if ((patternIndex > 0 || (patternIndex == 0 && (rotateDir == patternDir || rotations != 0))))
                            rotations += (int)rotateDir;
                        if (rotations != 0) CheckCombination();
                    }
                    else CheckCombination();
                }
            }
        }
    }

    private void CheckCombination()
    {
        bool increasePattern = fullRotations ? rotations / triggerAmount == Pattern[patternIndex] : triggerIndex == Pattern[patternIndex];
        if (increasePattern)
        {
            patternIndex++;
            if (ClickSound) audioSource.PlayOneShot(ClickSound);
            if (patternIndex == Pattern.Length)
            {
                openEvent.Invoke();
                return;
            }
            patternDir = (RotationDirection)((int)patternDir * -1);
        }
        else if (rotateDir != patternDir)
        {
            if (!fullRotations) rotations = 0;
            patternIndex = 0;
            patternDir = (Pattern[patternIndex] > 0) ? RotationDirection.ClockWise : RotationDirection.CounterCW;
        }
    }
}

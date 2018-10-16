using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Crank : MonoBehaviour
{
    private enum CrankDirection
    {
        ClockWise = 1,
        CounterCW = -1,
    }

    [Header("Crank")]
    [SerializeField]
    private JackInBox jackInBox;
    [SerializeField]
    private Transform attachPoint;

    [Space(10)]
    [SerializeField, Range(-5, 5)]
    private int[] CrankPattern;
    [SerializeField]
    private List<Collider> rotationTriggers = new List<Collider>();

    [Header("Audio")]
    [SerializeField]
    AudioClip CrankSound;
    [SerializeField]
    AudioClip MusicBox;

    private Rigidbody rb;
    private AudioSource audioSource;
    private CrankDirection patternDir;
    private CrankDirection rotateDir;
    
    private int patternIndex = 0;
    private int triggerIndex = 1;
    private int rotations = 0;

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

                audioSource.pitch = 0;
                audioSource.clip = MusicBox;
                audioSource.Play();
            }
            else
            {
                rb.isKinematic = false;
                transform.parent = null;
                audioSource.Stop();
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        patternDir = (CrankPattern[patternIndex] > 0) ? CrankDirection.ClockWise : CrankDirection.CounterCW;
    }

    private void Update()
    {
        audioSource.pitch = rb.angularVelocity.magnitude;
        if (prevRotation - transform.localEulerAngles.y > 10)
            audioSource.PlayOneShot(CrankSound);
    }

    private void CheckPattern()
    {
        if (rotations / 4 == CrankPattern[patternIndex])
        {
            patternIndex++;
            if (patternIndex == CrankPattern.Length)
            {
                jackInBox.Open();
                return;
            }
            patternDir = (CrankDirection)((int)patternDir * -1);
        }
        else if (rotateDir != patternDir)
        {
            rotations = 0;
            patternIndex = 0;
            patternDir = (CrankDirection)((int)patternDir * -1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Attached && patternIndex < CrankPattern.Length)
        {
            if (rotationTriggers.Contains(other))
            {
                int otherIndex = (triggerIndex < rotationTriggers.Count / 2 && triggerIndex >= 0) ? rotationTriggers.IndexOf(other) : rotationTriggers.LastIndexOf(other);
                if (otherIndex != triggerIndex)
                {
                    rotateDir = (otherIndex - triggerIndex > 0) ? CrankDirection.ClockWise : CrankDirection.CounterCW;
                    rotations += (int)rotateDir;

                    triggerIndex += (int)rotateDir;
                    if (triggerIndex > rotationTriggers.Count - 2) triggerIndex = 1;
                    if (triggerIndex < 1) triggerIndex += rotationTriggers.Count - 2;
                    if (triggerIndex == 1) CheckPattern();
                }
            }
        }
    }
}

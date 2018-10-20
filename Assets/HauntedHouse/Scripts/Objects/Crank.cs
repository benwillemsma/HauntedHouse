using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : RadialCombination
{
    [Header("Crank")]
    [SerializeField]
    private Transform attachPoint;

    [SerializeField]
    private bool detach; // for testing
	
	protected override void Update ()
    {
        if (detach)
        {
            detach = false;
            Attached = false;
        }
        base.Update();
    }

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

    protected override void OnTriggerEnter(Collider other)
    {
        if (Attached) base.OnTriggerEnter(other);
        else if (!Attached && other.transform == attachPoint)
            Attached = true;
    }
}

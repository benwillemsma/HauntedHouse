using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    private Transform mainTransform;
    public Transform legBase;
    public Transform bone1;
	public Transform bone2;
	public Transform endPoint;
	public Transform target;
    public Transform hintPos;

    [Space(20)]
    public Vector3 targetOffset = new Vector3(0, 0.5f, 0);

    [Space(20)]
	public Vector3 bone1_OffsetRotation;
	public Vector3 bone2_OffsetRotation;
	public Vector3 endPoint_OffsetRotation;

    private void Start()
    {
        mainTransform = transform.root;
    }

    public void LateUpdate()
    {
		if(bone1 != null && bone2 != null && endPoint != null && target != null)
        {
            target.LookAt(mainTransform);
            Ray ray = new Ray(target.transform.position + mainTransform.up * 2, -mainTransform.up);
            RaycastHit hit;
            Vector3 desiredPos = target.position;
            
            if (Physics.Raycast(ray, out hit, 5f, 1 << 8, QueryTriggerInteraction.Ignore))
            {
                float stepHeight = mainTransform.InverseTransformDirection(target.position - hit.point + (mainTransform.rotation * targetOffset)).y;
                if (stepHeight <= 0) desiredPos = hit.point;
                else desiredPos = Vector3.Lerp(hit.point, target.position, stepHeight);
            }

            target.position = desiredPos + (mainTransform.rotation * targetOffset);

            float bone1_Length = Vector3.Distance(bone1.position, bone2.position);
            float forearm_Length = Vector3.Distance(bone2.position, endPoint.position);

            Vector3 baseToFoot = target.position - legBase.position;
            legBase.LookAt(legBase.position + Vector3.ProjectOnPlane(baseToFoot, mainTransform.up));

            Vector3 hint = (bone1.position + endPoint.position) / 2 + Vector3.up;
            if (hintPos) hint = hintPos.position;
            else if (mainTransform) hint = (bone1.position + endPoint.position) / 2 + mainTransform.up;

            bone1.LookAt (target, hint - bone1.position);
			bone1.Rotate (bone1_OffsetRotation);

			Vector3 cross = Vector3.Cross (hint - bone1.position, bone2.position - bone1.position);
            
            float totalLength = bone1_Length + forearm_Length;
            float targetDistance = Vector3.Distance (bone1.position, target.position);
			targetDistance = Mathf.Min (targetDistance, totalLength - totalLength * 0.001f);

            float adjacent = ((bone1_Length * bone1_Length) - (forearm_Length * forearm_Length) + (targetDistance * targetDistance)) / (2*targetDistance);
            float angle = Mathf.Acos (adjacent / bone1_Length) * Mathf.Rad2Deg;

			bone1.RotateAround (bone1.position, cross, -angle);

            bone2.LookAt(target, cross);
            bone2.Rotate (bone2_OffsetRotation);	
		}
	}
}

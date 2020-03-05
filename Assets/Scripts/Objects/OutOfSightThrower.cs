using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfSightThrower : ItemSpawner
{
    [Header("Throw Settings")]
    public Vector2 throwForce = new Vector2(1, 1);
    public Transform[] targets;

    protected override void SpawnItem(GameObject triggerObject)
    {
        Vector3 objectOffset = (transform.position - triggerObject.transform.position);
        objectOffset.y = 0;
        
        if (!hasItem && Mathf.Abs(Vector3.Dot(objectOffset.normalized, transform.forward)) < 0.9f)
        {
            objectOffset *= ((Random.value > 0.5f) ? 1 : -1);
            spawnPoint.position = transform.position + Vector3.Cross(objectOffset.normalized, Vector3.up).normalized * objectOffset.magnitude * 2;

            hasItem = true;
            item = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity, transform);

            Transform selectedTarget = transform;
            for (int i = 0; i < targets.Length; i++)
            {
                float angle = Mathf.Abs(Vector3.SignedAngle(targets[i].forward, spawnPoint.position - targets[i].transform.position, Vector3.up));
                if (angle < 80)
                {
                    selectedTarget = targets[i];
                    break;
                }
            }
            Vector3 throwDirection = selectedTarget.position - spawnPoint.position;
            throwDirection.y = 0;

            Destroy(item.gameObject, itemLifeTime);

            item.AddForce(throwDirection * throwForce.x + Vector3.up * throwForce.y, ForceMode.Impulse);
        }
    }
}

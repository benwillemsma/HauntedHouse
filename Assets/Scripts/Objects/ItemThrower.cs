using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrower : ItemSpawner
{
    [Header("Throw Settings")]
    public Vector2 throwForce = new Vector2(1, 1);

    protected override void SpawnItem(GameObject triggerObject)
    {
        Vector3 objectOffset = (transform.position - triggerObject.transform.position);
        objectOffset.y = 0;
        
        hasItem = true;
        item = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity, transform);

        Vector3 throwDirection = transform.position - spawnPoint.position;
        throwDirection.y = 0;

        Destroy(item.gameObject, itemLifeTime);

        item.AddForce(throwDirection * throwForce.x + Vector3.up * throwForce.y, ForceMode.Impulse);
    }
}

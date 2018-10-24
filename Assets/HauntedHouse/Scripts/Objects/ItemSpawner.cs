using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float cooldown = 10;
    public float itemLifeTime = 10;

    public LayerMask layers;
    public Transform spawnPoint;
    public Rigidbody itemPrefab;

    protected bool hasItem = false;
    protected Rigidbody item;
    protected float timer = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (itemPrefab && layers == (layers | (1 << other.gameObject.layer)))
            SpawnItem(other.gameObject);
    }

    protected virtual void SpawnItem(GameObject triggerObject)
    {
        hasItem = true;
        item = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity, transform);
    }

    private void Update()
    {
        if (hasItem && !item)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;
                hasItem = false;
            }
        }
    }
}

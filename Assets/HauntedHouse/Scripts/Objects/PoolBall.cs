using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PoolBall : MonoBehaviour
{
    public Transform boundryCenter;
    public float boundryDistance = 4;

    private Rigidbody rb;
    private Vector3 startPos;

	void Start ()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        if(boundryCenter && (transform.position - boundryCenter.position).magnitude > boundryDistance)
            rb.velocity = Vector3.zero + Vector3.up * rb.velocity.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody other = collision.gameObject.GetComponent<Rigidbody>();
        if (other)
        {
            Vector3 normal = collision.contacts[0].normal;
            float force = Vector3.Project(rb.velocity, normal).magnitude * 0.8f;
            other.AddForce(normal * -force, ForceMode.Impulse);
            other.AddForce(normal * force, ForceMode.Impulse);
        }
        else rb.velocity = Vector3.Reflect(rb.velocity, collision.contacts[0].normal);
    }

    public void ResetBall()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(true);
    }
}

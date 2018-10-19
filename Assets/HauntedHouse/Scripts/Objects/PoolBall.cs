using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PoolBall : MonoBehaviour
{
    public Transform boundaryCenter;
    public float boundaryDistance = 2;
    public float boundaryTimer = 4;
    private float elapsedTime;

    private Rigidbody rb;
    private Vector3 startPos;

	void Start ()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        if (boundaryCenter && (transform.position - boundaryCenter.position).magnitude > boundaryDistance)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > boundaryTimer)
                ResetBall();
        }
        else elapsedTime = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody other = collision.gameObject.GetComponent<Rigidbody>();

        Vector3 normal = collision.contacts[0].normal;
        float force = Vector3.Project(rb.velocity, normal).magnitude;
        if (other)
        {
            force *= 0.8f;
            other.AddForce(normal * -force, ForceMode.Impulse);
            rb.AddForce(normal * force, ForceMode.Impulse);
        }
        else rb.AddForce(normal * force * 1.8f, ForceMode.Impulse);
    }

    public void ResetBall()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(true);
    }
}

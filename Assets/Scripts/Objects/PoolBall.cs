using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PoolBall : MonoBehaviour
{
    [SerializeField]
    private Transform boundaryCenter;
    [SerializeField]
    private float boundaryDistance = 2;
    [SerializeField]
    private float boundaryTimer = 4;
    private float elapsedTime;

    [Header("Audio")]
    [SerializeField]
    private AudioClip ballCollision;
    [SerializeField]
    private AudioClip sideCollision;
    private AudioSource audioS;

    private Rigidbody rb;
    private Vector3 startPos;

	void Start ()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>();
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
        if (other && other.CompareTag("PoolBall"))
        {
            force *= 0.8f;
            other.AddForce(normal * -force, ForceMode.Impulse);
            rb.AddForce(normal * force, ForceMode.Impulse);
            audioS.PlayOneShot(ballCollision);
        }
        else if (collision.collider.gameObject.CompareTag("PoolTable"))
        {
            rb.AddForce(normal * force * 1.8f, ForceMode.Impulse);
            audioS.PlayOneShot(sideCollision);
        }
    }

    public void ResetBall()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(true);
    }
}

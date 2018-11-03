using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTable : MonoBehaviour
{
    public List<PoolBall> balls;
    public int[] BallOrder = new int[16];

    public GameObject key;

    private int orderIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        PoolBall ball;
        if (ball = other.GetComponent<PoolBall>())
        {
            if (balls.IndexOf(ball) + 1 == BallOrder[orderIndex])
            {
                ball.gameObject.SetActive(false);
                orderIndex++;
                if (orderIndex >= BallOrder.Length)
                    RewardKey();
            }
            else
            {
                Rigidbody body = ball.GetComponent<Rigidbody>();
                body.velocity = Vector3.zero;
                Vector3 dir = ball.transform.parent.position - ball.transform.position;
                dir.y = 0;
                body.AddForce((Vector3.up * 2 + dir), ForceMode.Impulse);
            }
        }
    }

    private void RewardKey()
    {
        if (key) key.SetActive(true);
        Debug.Log("Key Rewarded");
    }
}

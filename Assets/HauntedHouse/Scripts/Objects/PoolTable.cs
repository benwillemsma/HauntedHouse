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
                for (int i = 0; i < balls.Count; i++)
                    balls[i].ResetBall();
            }
        }
    }

    private void RewardKey()
    {
        if (key) key.SetActive(true);
        Debug.Log("Key Rewarded");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public Rigidbody door;
    private bool Locked = true;

    public void Open()
    {
        if (Locked)
        {
            Locked = false;
            if (door)
            {
                door.AddForce(transform.right * 5, ForceMode.Impulse);
                //door.GetComponent<CircularDrive>().minAngle = -180;
            }
        }
    }
}

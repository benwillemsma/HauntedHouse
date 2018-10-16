using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class JackInBox : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        anim.SetBool("Open", true);
        Debug.Log("Jack in the box Opened");
    }
}

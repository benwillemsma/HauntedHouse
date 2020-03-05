using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class JackInBox : MonoBehaviour
{
    private Animator anim;
    private bool open = false;
    private bool hasKey = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Open()
    {
        open = true;
        anim.SetBool("Open", true);
        Debug.Log("Jack in the box Opened");
    }

    public void Unlock()
    {
        hasKey = true;
        anim.SetBool("Unlock", true);
        Debug.Log("Jack in the box Unlocked");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (open && hasKey && other.CompareTag("MoonLight"))
        {
            // Free Monster
        }
    }
}

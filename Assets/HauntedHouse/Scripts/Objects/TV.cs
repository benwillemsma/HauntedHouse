using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Animator))]
public class TV : MonoBehaviour
{
    [SerializeField, Range(0, 50)]
    private float goalRange = 10;

    [SerializeField]
    private GameObject key;

    [SerializeField,Space(10)]
    private CircularDrive[] dials;

    private float[] dialGoals;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        dialGoals = new float[dials.Length];
        SetGoals();
    }

    private void SetGoals()
    {
        for (int i = 0; i < dialGoals.Length; i++)
            dialGoals[i] = Random.Range(dials[i].minAngle, dials[i].maxAngle);
    }

    private void Update()
    {
        bool withinRange = true;
        for (int i = 0; i < dials.Length; i++)
        {
            float delta = Mathf.Abs(dials[i].outAngle - dialGoals[i]);
            if (delta > goalRange)
                withinRange = false;
            anim.SetFloat("Dial" + i, goalRange - delta);
        }
        if (withinRange) RewardKey();
    }

    private void RewardKey()
    {
        if (key) key.SetActive(true);
        Debug.Log("Key Rewarded");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class TV : MonoBehaviour
{
    [SerializeField, Range(0, 50)]
    private float goalRange = 10;

    [SerializeField]
    private GameObject key;

    [SerializeField,Space(10)]
    private CircularDrive[] dials;

    private float[] dialGoals;
    private Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().materials[1];
        mat.color = Color.red;


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
            if (Mathf.Abs(dials[i].outAngle - dialGoals[i]) > goalRange)
                withinRange = false;
            else Debug.Log(dials[i].gameObject + ": is Within Goal Range");
        }
        if (withinRange) RewardKey();
    }

    private void RewardKey()
    {
        if (key) key.SetActive(true);
        Debug.Log("Key Rewarded");
    }
}

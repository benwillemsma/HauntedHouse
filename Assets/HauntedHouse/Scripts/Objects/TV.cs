using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public Transform SaturationDial;
    public Transform ColorDial;

    public GameObject key;

    private Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().materials[1];
        mat.color = Color.red;
    }

    private void Update()
    {
        float saturation = SaturationDial.localEulerAngles.z;
        float color = ColorDial.localEulerAngles.z;

        Color newColor = Color.Lerp(Color.clear, Color.red, color / 360);
        newColor.r = Mathf.Lerp(0, 1, saturation / 360);
        mat.color = newColor;

        mat.mainTextureOffset = new Vector3(Random.Range(0, 100), Random.Range(0, 100));

        if (color > 300 && saturation > 300)
            RewardKey();
    }

    private void RewardKey()
    {
        if (key) key.SetActive(true);
        Debug.Log("Key Rewarded");
    }
}

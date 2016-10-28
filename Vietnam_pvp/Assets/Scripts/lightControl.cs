using UnityEngine;
using System.Collections;

public class lightControl : MonoBehaviour
{
    public dayNight night;
    public bool hasDaynight;
    public Color color0;
    public Color color1;

    public float minFlickerIntensity;
    public float maxFlickerIntensity;
    public float ammount;

    public Light lt;

    void Start()
    {        
        lt = GetComponent<Light>();
        if(night == null && hasDaynight)
        {
            night = GameObject.FindGameObjectWithTag("sun").GetComponent<dayNight>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PerlinNoise(ammount, Time.time);
        lt.color = Color.Lerp(color0, color1, t);
        lt.range = Mathf.Lerp(minFlickerIntensity, maxFlickerIntensity, t);


        if (hasDaynight)
        {
        if (night.day == true)
        {
            lt.enabled = false;
        }
        else lt.enabled = true;
        }


    }
}

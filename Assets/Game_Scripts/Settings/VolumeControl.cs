using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeControl : MonoBehaviour {

    float minValue = 0f, maxValue = 1f, variationAmount = .1f;
    Slider sliderRef;

    void Start()
    {
        sliderRef = this.gameObject.GetComponent<Slider>();
        sliderRef.minValue = minValue;
        sliderRef.maxValue = maxValue;
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {

            sliderRef.value -= variationAmount;
        }
        else if (Input.GetKeyDown("x"))
        {
            sliderRef.value += variationAmount;
        }
    }
}

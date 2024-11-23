using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    Slider slider;
    public string blenderShapeName;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(value=>ShapeChange.Instance.ChangeBlenderShapeValue(blenderShapeName,value));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{
    float _value;
    public float Value { 
        get {
            return _value;
        }
        set {
            _value = value;
            slider.value = value;
        }
    }


    [SerializeField] Slider slider;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{
    bool hasBeenSaneForFirstTime = false;

    bool debugDisableMicrophone = false;
    bool debugDontDeplete = false;

    float _value = 1f; // 0.0 to 1.0
    public float Value { 
        get {
            return _value;
        }
        set {
            _value = value;
            _value = Mathf.Clamp01(_value);
            UpdateUI();
        }
    }
    float _prevSanity = 1f;

    [Header("References")]
    [SerializeField] GameObject visiblityParent;
    [SerializeField] Slider slider;
    [SerializeField] Image redBg;
    [SerializeField] MicrophoneManager micMgr;
    [SerializeField] SceneSwitch sceneSwitch;
    [Header("Settings / Values")]
    [SerializeField] float laughRecoverySpeed = 0.35f;
    [SerializeField] float drainSpeed = 0.01f;
    [SerializeField] int maxRandFlashChance = 150;

    void UpdateUI() {
        slider.value = _value;

        float redAlpha = Mathf.Clamp01((1f - _value) - 0.5f) * 2f;

        redBg.color = new Color(redBg.color.r, redBg.color.g, redBg.color.b, redAlpha);
    }


    void Update() {
        if(debugDontDeplete) return;
        
        Value -= Time.deltaTime * drainSpeed;

        if(micMgr.isLaughing && hasBeenSaneForFirstTime && !debugDisableMicrophone) {
            Value += Time.deltaTime * laughRecoverySpeed;
        }


        if(_prevSanity > 0.8f && Value < 0.8f) { // force a flash at the 0.7f mark
            sceneSwitch.FlashHellForTime(0.05f);
            print("cross 0.8f");
        }

        sceneSwitch.IsHappy = Value > 0.5f;

        if(!sceneSwitch.IsHappy && !hasBeenSaneForFirstTime) {
            hasBeenSaneForFirstTime=true;
            visiblityParent.SetActive(true);
        }


        _prevSanity = Value;
    }

    void FixedUpdate() {
        if(Value < 0.66f && Value > 0.5f) {
            if(Random.Range(0, maxRandFlashChance / ((Value < 0.53f)?10:1)) == 0) {
                sceneSwitch.FlashHellForTime(0.05f);
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sanity : MonoBehaviour
{
    public static Sanity Instance;
    void Awake() {
        if(Instance){
            Destroy(this);
        }
        else {
            Instance=this;
        }
    }

    bool hasBeenSaneForFirstTime = false;

    [SerializeField] bool debugDisableMicrophone = false;
    [SerializeField] bool debugDontDeplete = false;

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

    int _happyTasksComplete = 0;
    public int HappyTasksComplete {
        get {
            return _happyTasksComplete;
        }
        private set {
            _happyTasksComplete = value;

            happyCounter.text = _happyTasksComplete.ToString();
        }
    }


    public bool IsSane {
        get {
            return Value <= 0.5f;
        }
        private set {}
    }

    [Header("References")]
    [SerializeField] GameObject visiblityParent;
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text happyCounter;
    [SerializeField] Image redBg;
    [SerializeField] SceneSwitch sceneSwitch;
    [Header("Settings / Values")]
    // [SerializeField] float laughRecoverySpeed = 0.35f;
    [SerializeField] float drainSpeed = 0.01f;
    [SerializeField] float firstTimeDrainSpeed = 0.03f;
    [SerializeField] int maxRandFlashChance = 150;
    Gun gun;

    void Start() {
        gun = GameObject.FindGameObjectWithTag("PlayerCamTransform").GetComponentInChildren<Gun>();
    }

    void UpdateUI() {
        slider.value = _value;

        float redAlpha = Mathf.Clamp01((1f - _value) - 0.5f) * 2f;

        redBg.color = new Color(redBg.color.r, redBg.color.g, redBg.color.b, redAlpha);
    }


    void Update() {
        if(debugDontDeplete) return;
        
        if(hasBeenSaneForFirstTime)
        {
            Value -= Time.deltaTime * drainSpeed * ((Value<0.5f)?0.5f:1f) * ((gun.Ammo<=0.0f&&Value>0.5f)?30f:1f);
        }
        else
        {
            Value -= Time.deltaTime * firstTimeDrainSpeed;
        }

        // if(micMgr.isLaughing && hasBeenSaneForFirstTime && !debugDisableMicrophone) {
        //     Value += Time.deltaTime * laughRecoverySpeed;
        // }


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

    public void RegisterKill() {
        HappyTasksComplete++;


        if(!hasBeenSaneForFirstTime) return; // Only start increasing if the player has seen hell already

        // Value += 0.4f;
        Value = 1f;
    }
}

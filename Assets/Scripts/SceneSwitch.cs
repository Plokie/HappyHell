using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] RawImage rawImageTarget;
    [SerializeField] RenderTexture happyRT;
    [SerializeField] RenderTexture hellRT;


    [SerializeField] bool _isHappy = true;
    public bool IsHappy {
        get {
            return _isHappy;
        }
        set {
            _isHappy = value;
            SetRT();
        }
    }
    // int forceHellCount = 0;
    float forceHellTimer = 0.0f;

    void SetRT(bool? forceHappy = null) {
        RenderTexture currentRT = IsHappy ? happyRT : hellRT;

        if(forceHappy.HasValue) {
            currentRT = forceHappy.Value ? happyRT : hellRT;
        }
        
        rawImageTarget.texture = currentRT;
    }

    // public void FlashHellForFrames(int frameCount) {
    //     forceHellCount = frameCount;
    // }

    public void FlashHellForTime(float time) {
        forceHellTimer = time;
    }

    void LateUpdate() {
        if(Input.GetKeyDown(KeyCode.P)) {
            IsHappy = !IsHappy;

            SetRT();
        }

        // if(Input.GetKeyDown(KeyCode.L)) {
        //     forceHellCount = 8;
        // }



        //Leave at end
        // if(forceHellCount>=0) {
        //     SetRT(forceHellCount==0); //if its the last frame of forced hell frames, set back to happy

        //     forceHellCount--;
        // }

        if(forceHellTimer > 0.0f) {
            SetRT(false);

            forceHellTimer -= Time.deltaTime;
        }
    }

    void OnValidate() {
        SetRT();
    }
}

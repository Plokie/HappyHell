using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] RawImage rawImageTarget;
    [SerializeField] RenderTexture happyRT;
    [SerializeField] RenderTexture hellRT;

    public bool isHappy = true;
    int forceHellCount = 0;

    void SetRT(bool? forceHappy = null) {
        RenderTexture currentRT = isHappy ? happyRT : hellRT;

        if(forceHappy.HasValue) {
            currentRT = forceHappy.Value ? happyRT : hellRT;
        }
        
        rawImageTarget.texture = currentRT;
    }

    public void FlashHellForFrames(int frameCount) {
        forceHellCount = frameCount;
    }

    void LateUpdate() {
        if(Input.GetKeyDown(KeyCode.P)) {
            isHappy = !isHappy;

            SetRT();
        }

        if(Input.GetKeyDown(KeyCode.L)) {
            forceHellCount = 5;
        }



        //Leave at end
        if(forceHellCount>=0) {
            SetRT(forceHellCount==0); //if its the last frame of forced hell frames, set back to happy

            forceHellCount--;
        }
    }
}

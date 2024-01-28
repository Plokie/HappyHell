using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public Sanity sanity; 
    public UnityEngine.UI.Image ammoSlider;
    //Tutoral int
    //0 = Doesn't exist yet
    //1 = Hasn't completed tutorial
    //2 = Tutorial completed
    void Start()
    {
        float timer = 0;
        Color originalColor = ammoSlider.color;
        if(PlayerPrefs.GetInt("Tutorial", 0) == 0){
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.Save();
        }

        Objective.QueueObjective("Walking", "Use WASD to walk around!", ()=>{
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)) return true;
            return false;
        });
        Objective.QueueObjective("Happy", "Hold Left Mouse button to make the person in front of you happy!", ()=>{
            if(Input.GetMouseButton(0)) return true;
            return false;
        });
        Objective.QueueObjective("Laugh", "Laugh to make them happier faster!", ()=>{
            return sanity.HappyTasksComplete > 0;
        });
        Objective.QueueObjective("Canister", "Don't let your Happy Gas™ Run out!", ()=>{
            timer += Time.deltaTime;
            if (timer > 0.5f){
                ammoSlider.color = Color.green;
            }
            
            if (timer > 3f){
                ammoSlider.color = originalColor;
            }


            return timer > 4;
        });

        // leave at end
        Objective.QueueObjective("Final", "Nearest Person", typeof(NPC), () =>
        {
            return false;
        });

    }

    void Update()
    {
        
    }
}

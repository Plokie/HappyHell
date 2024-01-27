using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Animations;

public class NPC : MonoBehaviour
{

    public Animator npcHappy, npcHell;
    public bool Alive = true;

    
    void Update()
    {
        if (!Alive){
            npcHappy.SetBool("Alive", false);
            npcHell.SetBool("Alive", false);
        }
    }
}

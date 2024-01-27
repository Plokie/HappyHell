using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IShootable
{

    public Animator npcHappy, npcHell;
    public bool Alive = true;

    int Health = 100;

    void IShootable.BeingShot()
    {
        
    }

    void IShootable.StartShot()
    {

    }

    void Update()
    {
        if (!Alive){
            npcHappy.SetBool("Alive", false);
            npcHell.SetBool("Alive", false);
        }
        
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IShootable
{

    public Animator npcHappy, npcHell;
    public Slider healthBar, happyBar;
    public bool Alive = true;

    float Health = 100;

    void IShootable.BeingShot()
    {
        
    }

    void IShootable.StartShot()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!Alive){
            npcHappy.SetBool("Alive", false);
            npcHell.SetBool("Alive", false);
        }

        healthBar.value = Health;
        happyBar.value = Health;

        if (Health <= 0)
        {
            Alive = false;
        }

    }
}


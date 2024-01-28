using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IShootable
{

    public Animator npcHappy, npcHell;
    public Slider healthBar, happyBar;
    public bool Alive = true;
    Collider coll;
    [SerializeField] float baseDamage = 5f;
    [SerializeField] float laughingMultiplier = 5f;

    float Health = 100;

    void IShootable.BeingShot()
    {

       Health -= baseDamage * (MicrophoneManager.Instance.isLaughing?laughingMultiplier:1f) * Time.deltaTime;

    }

    void IShootable.StartShot()
    {

    }

    void Start()
    {
        coll = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (!Alive){

        }
        else {
            healthBar.value = Health;
            happyBar.value = Health;

            if (Health <= 0)
            {
                // on death
                Alive = false;
                coll.enabled=false;
                npcHappy.SetBool("Alive", false);
                npcHell.SetBool("Alive", false);
                Sanity.Instance.RegisterKill();
                //healthBar.gameObject.gameObject.SetActive(false);
                //happyBar.gameObject.SetActive(false);
            }
        }


    }
}


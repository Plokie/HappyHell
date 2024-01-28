using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCanvas : MonoBehaviour
{
    Transform playerCamTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerCamTransform = GameObject.FindGameObjectWithTag("PlayerCamTransform").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCamTransform) {
            transform.forward = (playerCamTransform.position - transform.position).normalized;
        }
    }
}

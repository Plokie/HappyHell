using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using TMPro;

public class ObjectivePointer : MonoBehaviour
{
    Transform playerCam;
    public ObjectiveQuest myQuest;

    [Header("Local references")]
    [SerializeField] Transform pointerPivot;
    [SerializeField] TMP_Text questText;

    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("PlayerCamTransform").transform;


        questText.text = myQuest.text;
        if(myQuest.objectiveType == ObjectiveType.MeetQuery)
        {
            pointerPivot.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(pointerPivot.gameObject.activeSelf)
        {
            Vector3 targetDir = (myQuest.targetPosition - playerCam.position).normalized;
            float angle = Vector3.Angle(playerCam.forward, targetDir);

            pointerPivot.localEulerAngles.Set(0, 0, angle);
        }
    }
}

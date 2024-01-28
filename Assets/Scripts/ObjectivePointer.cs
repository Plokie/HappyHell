using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using TMPro;

public class ObjectivePointer : MonoBehaviour
{
    Transform playerCam;
    Camera cam;
    public ObjectiveQuest myQuest;

    [Header("Local references")]
    [SerializeField] Transform pointerPivot;
    [SerializeField] GameObject visibilityParent;
    [SerializeField] TMP_Text questText;

    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("PlayerCamTransform").transform;
        cam = playerCam.GetComponentInChildren<Camera>();
            
        questText.text = myQuest.text;
        if(myQuest.objectiveType == ObjectiveType.MeetQuery)
        {
            pointerPivot.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //lazy
        
        if(myQuest.objectiveType!=ObjectiveType.MeetQuery)
        {
            Vector3 viewportPoint = cam.WorldToViewportPoint(myQuest.targetPosition);
            //print(viewportPoint);
            if (viewportPoint.x > 0f && viewportPoint.x < 1f && viewportPoint.y > 0f && viewportPoint.y < 1f && viewportPoint.z>0f)
            {
                visibilityParent.SetActive(false);
            }
            else
            {
                visibilityParent.SetActive(true);
                var targetPosLocal = playerCam.InverseTransformPoint(myQuest.targetPosition);
                var targetAngle = -Mathf.Atan2(targetPosLocal.x, targetPosLocal.y) * Mathf.Rad2Deg + 90;

                //Vector3 targetDir = (myQuest.targetPosition - playerCam.position).normalized;
                //float angle = Vector3.Angle(playerCam.forward, targetDir);

                pointerPivot.localEulerAngles = new Vector3(0, 0, targetAngle);
            }


        }
    }
}

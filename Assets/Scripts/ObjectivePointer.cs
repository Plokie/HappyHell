using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class ObjectivePointer : MonoBehaviour
{
    Transform playerCam;
    public ObjectiveQuest myQuest;

    [Header("Local references")]
    [SerializeField] Transform pointerPivot;

    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("PlayerCamTransform").transform;
    }

    private void Update()
    {
        Vector3 targetDir = (myQuest.targetPosition - playerCam.position).normalized;
        float angle = Vector3.Angle(playerCam.forward, targetDir);

        pointerPivot.localEulerAngles.Set(0, 0, angle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    //[SerializeField] float shakeAmount = 10.0f;
    //[SerializeField] float smoothSpeed = 10.0f;
    //Vector3 prevCamRot = Vector3.zero;
    //Quaternion defaultRotation = Quaternion.identity;

    //Vector3 smoothRotationDelta = Vector3.zero;
    //Transform playerCamTransform = null;


    //private void Start()
    //{
    //    defaultRotation = transform.localRotation;
    //    playerCamTransform = GameObject.FindGameObjectWithTag("PlayerCamTransform").transform;
    //}

    //private void Update() {


    //    //smoothMouseDelta = Vector3.Lerp(smoothMouseDelta, mouseDelta, Time.deltaTime * smoothSpeed);

    //    //Quaternion.Lerp()
    //    Vector3 rotDelta = prevCamRot - playerCamTransform.rotation.eulerAngles;
    //    smoothRotationDelta = Vector3.Lerp(smoothRotationDelta, rotDelta, Time.deltaTime * smoothSpeed);

    //    Vector3 rot = new Vector3(smoothRotationDelta.y, smoothRotationDelta.x, 0) * Time.deltaTime * shakeAmount;

    //    //transform.localEulerAngles = defaultRotation.eulerAngles + 
    //    transform.localRotation = defaultRotation;
    //    transform.Rotate(rot, Space.Self);


    //    prevCamRot = playerCamTransform.rotation.eulerAngles;
    //}
}

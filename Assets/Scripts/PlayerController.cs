using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;
    public CinemachineVirtualCamera vcam;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
        
    [SerializeField, SaveDuringPlay]
    private float playerSpeed = 5.0f;

    [SerializeField, SaveDuringPlay]
    private float jumpHeight = 5.0f;
    
    [SerializeField, SaveDuringPlay]
    private float gravityValue = -9.81f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {

        float yRotation = cameraTransform.eulerAngles.y;
        yRotation = Mathf.Repeat(yRotation + 180f, 360f) - 180f; // Wrap yRotation between -180 and 180

        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, yRotation, gameObject.transform.rotation.eulerAngles.z);

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            playerVelocity.y = -2f;
        }


        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 side = transform.TransformDirection(Vector3.left);
        float fwdSpeed = playerSpeed * Input.GetAxis("Vertical");
        float sdeSpeed = playerSpeed * Input.GetAxis("Horizontal");
        controller.SimpleMove((forward * fwdSpeed) + -(side * sdeSpeed));

        if(fwdSpeed > 0.1){
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1 + (fwdSpeed / 4);
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1 + fwdSpeed + 0.5f;
        }
        else{
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        }


        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        

        
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

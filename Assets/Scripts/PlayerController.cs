using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
        
    [SerializeField, Range(0, 10)]
    private float playerSpeed = 2.0f;

    [SerializeField, Range(0, 20)]
    private float jumpHeight = 1.0f;
    
    [SerializeField, Range(-20, 0)]
    private float gravityValue = -9.81f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, cameraTransform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w);

        //print(playerVelocity.y);
        //print(groundedPlayer);

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

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        

        
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

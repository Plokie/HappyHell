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

        print(groundedPlayer);

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            playerVelocity.y = -controller.stepOffset / Time.deltaTime;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = playerSpeed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = jumpHeight;
        }
        else{
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

        
        controller.Move(playerVelocity * Time.deltaTime);
    }
}

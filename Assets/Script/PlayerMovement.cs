using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -19.62f;
    public float jumpHeight = 1f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded = false;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Check if isGrounded

        if(isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal"); //Get wasd input
        float z = Input.GetAxis("Vertical");

        if(Input.GetButton("SpeedUp")) speed = 8f;
        else speed = 5f;

        Vector3 move = transform.right * x * speed + transform.forward * z * speed + transform.up * velocity.y; //Put into vector 3

        controller.Move(move * Time.deltaTime); //Apply using controller

        if(Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); //Apply jumping force
        }

        velocity.y += gravity * Time.deltaTime; //Apply gravity over time
    }
}

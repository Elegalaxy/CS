using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float mouseSensitivity = 250f;

    public Transform playerBody;
    public Transform weapon;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor to middle of screen
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //Get input from mouse
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Avoid over rotate

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Apply camera rotation on Y
        playerBody.Rotate(Vector3.up * mouseX); //Apply body rotation on X
    }
}

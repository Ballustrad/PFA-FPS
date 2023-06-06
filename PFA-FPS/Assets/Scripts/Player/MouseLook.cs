using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;

    public Transform playerBody;

    [SerializeField] private float xRotation = 0f;
    private float maxVerticalAngle = 40f; // Angle vertical maximum en degrés

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

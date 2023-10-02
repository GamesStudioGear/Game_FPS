using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public float yOffset, yOffsetCrouched, moveDelay;
    public float sensitivity, rotationLimit, rotationDelay;

    private float mouseX, mouseY;
    private float rotX, rotY;
    private Transform playerTransform;

    void Start()
    {
        FPSProperties.canMoveCam = true;
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (FPSProperties.canMoveCam)
        {
            mouseX = Input.GetAxis("Mouse Y");
            mouseY = Input.GetAxis("Mouse X");

            rotX -= mouseX * sensitivity * Time.deltaTime;
            rotY += mouseY * sensitivity * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -rotationLimit, rotationLimit);

            transform.rotation = Quaternion.Euler(rotX, rotY, 0);
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, Quaternion.Euler(0, rotY, 0), rotationDelay * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + playerTransform.up * (FPSProperties.crouched? yOffsetCrouched : yOffset), moveDelay * Time.deltaTime);
    }
}

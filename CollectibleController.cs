using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private float rotX, rotY;
    public float sensitivity = 200;

    void Update()
    {
        RotateObject();
    }

    void RotateObject()
    {
        rotX = Input.GetAxis("Mouse X");
        rotY = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(rotY, rotX, 0) * Time.deltaTime * sensitivity);
    }
}

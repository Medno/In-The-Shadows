using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float horizontalSpeed = 1.0f;
    public float verticalSpeed = 1.0f;
    private Vector3 screenPoint;
    void Start()
    {

    }

    void    OnMouseDown()
    {
        screenPoint = new Vector3(0.0f, 0.0f, 0.0f);
    }
    void    OnMouseDrag()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            screenPoint.x = 0.0f;
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
            screenPoint.y = 0.0f;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            screenPoint.y += Input.GetAxis("Mouse Y");
        else
            screenPoint.x += Input.GetAxis("Mouse X");
        transform.Rotate(verticalSpeed * screenPoint.y * Time.deltaTime, horizontalSpeed * screenPoint.x * Time.deltaTime, 0);
    }
    void Update()
    {

    }
}

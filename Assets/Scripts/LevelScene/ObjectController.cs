using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class ObjectController : MonoBehaviour
{
    private float horizontalSpeed = 5.0f;
    private float verticalSpeed = 5.0f;
    private Vector3 screenPoint;
    [SerializeField]private Level level;
    void Start()
    {
        level = GetComponent<Object>().level;
    }
    void OnMouseDown()
    {
        screenPoint = new Vector3(0.0f, 0.0f, 0.0f);
    }
    void ResetOffset()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            screenPoint.x = 0.0f;
        else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
            screenPoint.y = 0.0f;

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            screenPoint.x = 0.0f;
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            screenPoint.y = 0.0f;
    }
    void MoveObject()
    {
        Vector3 limitPosition = transform.position;
        screenPoint.y += Input.GetAxis("Mouse Y");
        if (transform.position.y < 6.0f)
        {
            screenPoint.y = 0.0f;
            limitPosition.y = 6.0f;
        }
        else if (transform.position.y > 12.0f)
        {
            screenPoint.y = 0.0f;
            limitPosition.y = 12.0f;
        }
        else
            limitPosition.y += screenPoint.y * Time.deltaTime;
        transform.position = limitPosition;
    }
    void RotateObject()
    {
        if (level.currentDifficulty >= Level.difficulty.TWO
            && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            screenPoint.y += Input.GetAxis("Mouse Y");
        else
            screenPoint.x -= Input.GetAxis("Mouse X");
        transform.Rotate(verticalSpeed * screenPoint.y * Time.deltaTime, horizontalSpeed * screenPoint.x * Time.deltaTime, 0, Space.World);
    }
    void OnMouseDrag()
    {
        ResetOffset();
        if (level.currentDifficulty == Level.difficulty.THREE
            && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            MoveObject();
        else
            RotateObject();
    }
}

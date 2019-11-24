using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject[] levels;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    void FindCameraBounds()
    {
        if (levels[0])
        {
            xMin = levels[0].transform.position.x;
            xMax = levels[0].transform.position.x;
            yMin = levels[0].transform.position.y;
            yMax = levels[0].transform.position.y;
        }
        foreach(GameObject level in levels)
        {
            if (level.transform.position.x < xMin)
                xMin = level.transform.position.x;
            if (level.transform.position.x > xMax)
                xMax = level.transform.position.x;
            if (level.transform.position.y < yMin)
                yMin = level.transform.position.y;
            if (level.transform.position.y < yMax)
                yMax = level.transform.position.y;
        }
    }
    void Start()
    {
        levels = GameObject.FindGameObjectsWithTag("Level");
        FindCameraBounds();
    }
    void MoveCamera()
    {
        float speed = 2.0f;
        Vector3 offsetPosition = Vector3.zero;
        Vector3 castPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        // Debug.Log(castPoint.x);
        if (castPoint.x > 0.6f && transform.position.x < xMax + 1.0f)
            offsetPosition.x = (castPoint.x - 0.5f) * Time.deltaTime * speed;
        if (castPoint.x < 0.4f && transform.position.x > xMin - 1.0f)
            offsetPosition.x = (castPoint.x - 0.5f) * Time.deltaTime * speed;
        transform.position += offsetPosition;
    }
    void Update()
    {
        MoveCamera();
    }
}

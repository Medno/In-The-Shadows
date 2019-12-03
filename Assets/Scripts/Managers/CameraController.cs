using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject[] levels;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    void FindCameraBounds()
    {
        if (levels.Length > 0)
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
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        levels = GameObject.FindGameObjectsWithTag("Level");
        Debug.Log(levels.Length);
        FindCameraBounds();
        if (gameManager.selectedLevel)
            transform.position += Vector3.right * gameManager.selectedLevelPosition.x;
    }
    void MoveCamera()
    {
        float speed = 2.0f;
        Vector3 offsetPosition = Vector3.zero;
        Vector3 castPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
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

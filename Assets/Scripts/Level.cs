using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public enum status {
        Locked = 1 << 1,
        Available = 1 << 2,
        Done = 1 << 3
    };
    public enum difficulty {
        ONE, TWO, THREE
    };
    [System.Serializable]
    public struct LoadModels {
        public GameObject model;
        public Vector3 expectedPosition;
        public Vector3 expectedRotation;
        public Vector3 startPosition;
    };
    public string levelName;
    public string levelNameHint;
    public Level nextLevel;
    public difficulty currentDifficulty;
    public status levelStatus;
    public GameObject  objectPrefab;
    public LoadModels[]  modelDetails;
    private List<GameObject>  objectsInstantiated = new List<GameObject>();
    private List<Object>  objects = new List<Object>();
    public Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
    private CanvasDisplay eolevel;
    private Light[] spotlights;
    void InitObject()
    {
        for(int i = 0; i < modelDetails.Length; i++)
        {
            GameObject clone = Instantiate(objectPrefab, modelDetails[i].startPosition, Quaternion.identity);
            clone.transform.parent = gameObject.transform;
            clone.GetComponent<Object>().expectedPosition = modelDetails[i].expectedPosition;
            clone.GetComponent<Object>().expectedRotation = modelDetails[i].expectedRotation;
            GameObject cloneModel = Instantiate(modelDetails[i].model, modelDetails[i].startPosition, Quaternion.identity);
            cloneModel.transform.parent = clone.transform;
            clone.GetComponent<MeshCollider>().sharedMesh = null;
            clone.GetComponent<MeshCollider>().sharedMesh = cloneModel.GetComponentInChildren<MeshFilter>().mesh;
            objectsInstantiated.Add(clone);
            objects.Add(clone.GetComponent<Object>());
            clone.transform.localScale = scale;
        }
    }
    void Start()
    {
        eolevel = GameObject.FindGameObjectWithTag("End Of Level Canvas").GetComponent<CanvasDisplay>();
        spotlights = GameObject.FindObjectsOfType<Light>();
        InitObject();
    }
    void SaveProgression()
    {
        PlayerPrefs.SetInt(levelName + "_status", 2);
        if (nextLevel)
            PlayerPrefs.SetInt(nextLevel.levelName + "_status", 1);
        PlayerPrefs.Save();
    }
    IEnumerator ValidationLevelAnimation()
    {
        foreach(Light light in spotlights)
        {
            if (light.gameObject.name == "Spot Light")
                light.GetComponent<Animator>().SetTrigger("EOL");
            else
                light.GetComponent<Animator>().SetTrigger("Larger");
        }
        yield return new WaitForSeconds(3.0f);
        eolevel.EnableCanvas();
    }
    void LevelDone()
    {
        if (levelStatus != status.Done)
        {
            levelStatus = status.Done;
            LevelManager.selectedLevel = gameObject;
            StartCoroutine(ValidationLevelAnimation());
            if (!LevelManager.testMode)
                SaveProgression();
        }
    }
    void CheckObjectValidation()
    {
        if (objects.Count > 0)
        {
            bool allFinished = true;
            foreach(Object obj in objects)
                if (!obj.finished)
                    allFinished = false;
            if (allFinished)
                LevelDone();
        }
    }
    void Update()
    {
        CheckObjectValidation();
    }
}

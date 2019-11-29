using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public enum status {
        Locked = 1 << 0,
        Available = 1 << 1,
        Done = 1 << 2
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
    public status levelStatus = status.Locked;
    public GameObject  objectPrefab;
    public LoadModels[]  modelDetails;
    private List<GameObject>  objectsInstantiated = new List<GameObject>();
    public List<Object>  objects = new List<Object>();
    public Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
    public int score = 0;
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
        InitObject();
    }
}

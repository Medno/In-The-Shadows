using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [HideInInspector] public Level level;
    private Light[] spotlights;
    private CanvasDisplay eolevel;
    private GameManager gameManager;
    [SerializeField] private bool finished;
    private List<GameObject> objsInstantiated = new List<GameObject>();
    private List<Object> objs = new List<Object>();
    void CreateObjects()
    {
        foreach (GameObject obj in level.objects)
        {
            GameObject instanciated = Instantiate(obj, obj.transform.position, Quaternion.identity);
            objs.Add(instanciated.GetComponent<Object>());
            instanciated.transform.parent = gameObject.transform;
            objsInstantiated.Add(instanciated);
        }
    }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        spotlights = GameObject.FindObjectsOfType<Light>();
        eolevel = GameObject.FindGameObjectWithTag("End Of Level Canvas").GetComponent<CanvasDisplay>();
        if (gameManager.selectedLevel)
            level = gameManager.selectedLevel;
        finished = false;
        CreateObjects();
    }
    void SaveProgression()
    {
        gameManager.EditLevelData(level);
        Debug.Log("Status of next Level : " + level.nextLevel.levelStatus);
        gameManager.EditLevelData(level.nextLevel);
        gameManager.SaveGame();
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
        finished = true;
        GetComponent<AudioSource>().Play();
        StartCoroutine(ValidationLevelAnimation());
        if (level.levelStatus != Level.status.Done)
        {
            level.levelStatus = Level.status.Done;
            level.nextLevel.levelStatus = Level.status.Available;
        }
        if (!LevelManager.testMode)
            SaveProgression();
    }
    void CheckObjectValidation()
    {
        if (objsInstantiated.Count > 0)
        {
            bool allFinished = true;
            foreach(Object obj in objs)
                if (!obj.finished)
                    allFinished = false;
            if (allFinished)
                LevelDone();
        }
    }
    void Update()
    {
        if (level != null && !finished)
            CheckObjectValidation();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [HideInInspector] public GameObject levelGO;
    private Level level;
    private Light[] spotlights;
    private CanvasDisplay eolevel;
    private GameManager gameManager;
    [SerializeField] private bool finished;
    void Awake()
    {
        if (LevelManager.selectedLevel)
            levelGO = Instantiate(LevelManager.selectedLevel, LevelManager.selectedLevel.transform.position, Quaternion.identity);
        else
            Debug.Log("There is no level to load");
    }
    void Start()
    {
        // if (levelGO != null)
            // level = levelGO.GetComponent<Level>();
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
        finished = false;
        spotlights = GameObject.FindObjectsOfType<Light>();
        eolevel = GameObject.FindGameObjectWithTag("End Of Level Canvas").GetComponent<CanvasDisplay>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }
    void SaveProgression()
    {
        // PlayerPrefs.SetInt(levelName + "_status", 2);
        // if (nextLevel)
        //     PlayerPrefs.SetInt(nextLevel.levelName + "_status", 1);
        // PlayerPrefs.Save();
        gameManager.EditLevelData(level);
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
            level.levelStatus = Level.status.Done;
        if (!LevelManager.testMode)
            SaveProgression();
    }
    void CheckObjectValidation()
    {
        if (level.objects.Count > 0)
        {
            bool allFinished = true;
            foreach(Object obj in level.objects)
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

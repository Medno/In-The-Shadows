using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private GameManager gameManager;
    public Level level;
    private Light[] spotlights;
    private CanvasDisplay eolCanvas;
    private FadePanel[] eolevel;
    public bool finished;
    private List<GameObject> objsInstantiated = new List<GameObject>();
    private List<Object> objs = new List<Object>();
    void CreateObjects()
    {
        foreach (GameObject obj in level.objects)
        {
            GameObject instanciated = Instantiate(obj, obj.transform.position, obj.transform.rotation);
            objs.Add(instanciated.GetComponent<Object>());
            objsInstantiated.Add(instanciated);
        }
    }
    void Awake()
    {
        GameObject gmGO = GameObject.FindGameObjectWithTag("Game Manager");
        if (gmGO != null)
        {
            gameManager = gmGO.GetComponent<GameManager>();
            if (gameManager.selectedLevel)
                level = gameManager.selectedLevel;
        }
        spotlights = GameObject.FindObjectsOfType<Light>();
        eolCanvas = GameObject.FindGameObjectWithTag("End Of Level Canvas").GetComponent<CanvasDisplay>();
        eolevel = eolCanvas.GetComponents<FadePanel>();
        finished = false;
        CreateObjects();
    }
    void SaveProgression()
    {
        gameManager.EditLevelData(level, Level.status.Done);
        if (level.nextLevel)
            gameManager.EditLevelData(level.nextLevel, Level.status.Available);
        gameManager.GetComponent<PlayerProgression>().Save();
    }
    void ActiveEOLCanvas(FadePanel.panelLinked link)
    {
        foreach(FadePanel panel in eolevel)
            if (panel.linked == link)
            {
                panel.StartFading();
                break;
            }
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
        eolCanvas.EnableCanvas();
        eolCanvas.GetComponent<EnableGO>().Enable();
        ActiveEOLCanvas(FadePanel.panelLinked.objectName);
        yield return new WaitForSeconds(3.0f);
        ActiveEOLCanvas(FadePanel.panelLinked.pauseMenu);
    }
    void LevelDone()
    {
        finished = true;
        if (gameManager && !gameManager.testMode)
            SaveProgression();
        if (!gameManager || !gameManager.GetComponent<PlayerSettings>().muteAudio)
            GetComponent<AudioSource>().Play();
        StartCoroutine(ValidationLevelAnimation());
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResetData : MonoBehaviour
{
    public Button   reset;
    public GameObject confirmation;
    private GameManager gm;
    IEnumerator DisplayConfirmation()
    {
        confirmation.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        EventSystem.current.SetSelectedGameObject(null);
        confirmation.SetActive(false);
    }
    void Reset()
    {
        gm.ResetData();
        StartCoroutine(DisplayConfirmation());
    }
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        reset.onClick.AddListener(Reset);
    }
}

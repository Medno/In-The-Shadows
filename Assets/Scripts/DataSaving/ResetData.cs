using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResetData : MonoBehaviour
{
    private Button   reset;
    public GameObject confirmation;
    private PlayerProgression progression;
    IEnumerator DisplayConfirmation()
    {
        confirmation.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        EventSystem.current.SetSelectedGameObject(null);
        confirmation.SetActive(false);
    }
    void Reset()
    {
        progression.Reset();
        StartCoroutine(DisplayConfirmation());
    }
    void Start()
    {
        progression = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerProgression>();
        reset = GetComponent<Button>();
        reset.onClick.AddListener(Reset);
    }
}

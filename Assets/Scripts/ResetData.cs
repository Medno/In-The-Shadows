using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetData : MonoBehaviour
{
    public Button   reset;
    public GameObject confirmation;
    IEnumerator DisplayConfirmation()
    {
        confirmation.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        confirmation.SetActive(false);
    }
    void Reset()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(DisplayConfirmation());
    }
    void Start()
    {
        reset.onClick.AddListener(Reset);
    }
}

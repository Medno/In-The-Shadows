using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    private GameManager gm;
    private TextMeshProUGUI score;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        score = GetComponent<TextMeshProUGUI>();
        if (gm)
            score.text = "Score : " + gm.score.ToString();
    }
}

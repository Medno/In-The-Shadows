using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBackground : MonoBehaviour
{
    public Sprite[] sprites;
    private Image background;
    void Start()
    {
        background = GetComponent<Image>();
        background.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}

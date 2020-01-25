using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        string songs = "Songs:\nSite: filmmusic.io\nKevin MacLeod\n- Chill, Cool vibes, Midnight Tale, Clean Soul, Covert Affair, Windswept, Past Sadness\n";
        string soundEffects = "Sound effects:\nSite: free-loops.com - Harp, Marimba Fm7 Jazz 75 Instrument\n";
        string textures = "Textures:\nSite: 3dtextures.me\n";
        text.text = songs + soundEffects + textures;
    }
}

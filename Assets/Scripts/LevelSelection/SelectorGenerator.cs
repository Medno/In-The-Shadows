using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorGenerator : MonoBehaviour
{
    public GameObject selectorPrefab;
    private GameManager gm;
    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        List<GameObject> list = new List<GameObject>();
        Vector3 position = Vector3.zero;
        foreach(Level lvl in gm.levels)
        {
            GameObject clone = Instantiate(selectorPrefab, position, Quaternion.identity);
            clone.transform.parent = transform;
            list.Add(clone);
            position.x += 2;
        }
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<SelectorCube>().level = gm.levels[i];
            list[i].name = gm.levels[i].levelName;
            if (i + 1 < list.Count)
                list[i].GetComponent<SelectorCube>().next = list[i + 1].GetComponent<SelectorCube>();
            else
                list[i].GetComponent<SelectorCube>().next = null;
        }
    }
}

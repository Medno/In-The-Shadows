using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material Array", menuName = "ListMaterials")]
public class ListMaterials : ScriptableObject
{
    public Material[] array;
}

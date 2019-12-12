using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaterialHandler : MonoBehaviour
{
    public ListMaterials materials;
    private MeshRenderer meshRenderer;
    void AssignMaterial()
    {
        Material[] newMaterials = meshRenderer.materials;
        newMaterials[0] = materials.array[Random.Range(0, materials.array.Length)];
        newMaterials[0].color = Color.white;
        meshRenderer.materials = newMaterials;
    }
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        AssignMaterial();
    }
}

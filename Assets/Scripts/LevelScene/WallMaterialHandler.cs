using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaterialHandler : MonoBehaviour
{
    public Material[] grounds;
    public Material[] walls;
    public Material[] ceiling;
    public MeshRenderer groundRenderer;
    public MeshRenderer leftWallRenderer;
    public MeshRenderer rightWallRenderer;
    public MeshRenderer ceilingRenderer;

    void AssignMaterial(MeshRenderer renderer, Material[] array)
    {
        Material[] materials = renderer.materials;
        materials[0] = array[Random.Range(0, array.Length)];
        materials[0].color = Color.white;
        renderer.materials = materials;
    }
    void Start()
    {
        AssignMaterial(groundRenderer, grounds);
        AssignMaterial(leftWallRenderer, walls);
        AssignMaterial(rightWallRenderer, walls);
        AssignMaterial(ceilingRenderer, ceiling);
    }
}

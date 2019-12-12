using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TillTexture : MonoBehaviour
{
    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetTextureScale("_MainTex", new Vector2(transform.localScale.x, transform.localScale.z));
    }
}

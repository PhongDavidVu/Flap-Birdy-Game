using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogEffect : MonoBehaviour
{
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // inspired from Unity document for post processing effect
    // https://docs.unity3d.com/ScriptReference/Graphics.Blit.html
    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destinationTexture)
    {
        Graphics.Blit(sourceTexture, destinationTexture, material);
    }
}

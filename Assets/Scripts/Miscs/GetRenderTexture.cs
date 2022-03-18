using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRenderTexture : MonoBehaviour
{
    [SerializeField]
    private RenderTexture _RenderTexture;

    void Update()
    {
        Shader.SetGlobalTexture("_RenderTexture", _RenderTexture);
    }
}

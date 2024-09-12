using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test04_PlayerShader : TestBase
{
    public SpriteRenderer[] spriteRenderers;

    [ColorUsage(false,true)]
    public Color color;

    Material[] materials;

    readonly int EmessionColor_Hash = Shader.PropertyToID("_EmissionColor");

    private void Start()
    {
        materials = new Material[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
            //materials[i] = spriteRenderers[i].sharedMaterial;
        }
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        materials[0].SetColor(EmessionColor_Hash, color);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        materials[1].SetColor(EmessionColor_Hash, color);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        materials[2].SetColor(EmessionColor_Hash, color);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/CreateCustomRenderPipeline")]
public class CustomScriptPineAsset : RenderPipelineAsset
{

    protected override RenderPipeline CreatePipeline()
    {
        return new CustomPipeline();
    }
}

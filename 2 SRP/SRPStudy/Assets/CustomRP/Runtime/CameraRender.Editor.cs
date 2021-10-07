using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

public partial class CameraRender : MonoBehaviour
{
    partial void DrawGizmos();
    partial void DrawUnsupportedShaders();
    partial void PrepareForSceneWindow();
    partial void PrepareBuffer();
    string SampleName { get; set; }
#if UNITY_EDITOR
    //SRP不支持的着色器标签
    static ShaderTagId[] legacyShaderTagIds = { new ShaderTagId("Always"), new ShaderTagId("ForwardBase"), new ShaderTagId("PrepassBase"), 
        new ShaderTagId("Vertex"), new ShaderTagId("VertexLMRGBM"), new ShaderTagId("VertexLM")};
    private static Material errorMaterial;
    /// <summary>
    /// 绘制SRP不支持的着色器类型
    /// </summary>
    partial void DrawUnsupportedShaders() {
        //错误粉色彩色
        if (errorMaterial == null) {
            errorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));
        }
        var drawingSettings = new DrawingSettings(legacyShaderTagIds[0], new SortingSettings(m_camera)) { overrideMaterial = errorMaterial};
        for (int i = 0; i < legacyShaderTagIds.Length; i++) {
            drawingSettings.SetShaderPassName( i, legacyShaderTagIds[i]);
        }
        var filteringSettings = FilteringSettings.defaultValue;
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
    }

    //绘制DrawGizmos
    partial void DrawGizmos()
    {
        if (Handles.ShouldRenderGizmos())
        {
            context.DrawGizmos(m_camera, GizmoSubset.PreImageEffects);
            context.DrawGizmos(m_camera, GizmoSubset.PostImageEffects);
        }
    }

    /// <summary>
    /// 在Game视图绘制的几何体也绘制到Scene视图中
    /// </summary>
    partial void PrepareForSceneWindow()
    {
        if (m_camera.cameraType == CameraType.SceneView)
        {
            //如果切换到了Scene视图，调用此方法完成绘制
            ScriptableRenderContext.EmitWorldGeometryForSceneView(m_camera);
        }
    }
    partial void PrepareBuffer()
    {
        //设置一下只有在编辑器模式下才分配内存
        Profiler.BeginSample("Editor Only");
        buffer.name = SampleName = m_camera.name;
        Profiler.EndSample();
    }
#else
	const string SampleName = bufferName;
#endif
}

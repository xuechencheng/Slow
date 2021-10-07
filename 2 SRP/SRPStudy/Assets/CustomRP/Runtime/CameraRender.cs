using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRender : MonoBehaviour
{
    const string bufferName = "Render Camera1";
    //CommandBuffer是一个容器，保存了要执行的渲染命令，name可以让我们在FrameDebugger中找到它
    CommandBuffer buffer = new CommandBuffer { name = bufferName};
    ScriptableRenderContext context;
    Camera m_camera;
    //存储剔除后视野内的结果数据
    CullingResults cullingResults;
    //Pass ID
    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

    public void Render(ScriptableRenderContext context, Camera camera) {
        this.context = context;
        this.m_camera = camera;
        //设置buffer缓冲区的名字
        PrepareBuffer();
        PrepareForSceneWindow();
        if (!Cull()) {
            return;
        }
        Setup();
        DrawVisiableGeometry();
        DrawUnsupportedShaders();
        DrawGizmos();
        Submit();
    }

    /// <summary>
    /// 剔除
    /// </summary>
    /// <returns></returns>
    private bool Cull()
    {
        ScriptableCullingParameters p;
        //获取要剔除的物体
        if (m_camera.TryGetCullingParameters(out p))
        {
            cullingResults = context.Cull(ref p);//剔除
            return true;
        }
        return false;
    }

    private void Setup() {
        //设置相机的属性和VP矩阵
        context.SetupCameraProperties(m_camera);
        //得到相机的clear flags
        CameraClearFlags flags = m_camera.clearFlags;
        //设置相机清除状态
        buffer.ClearRenderTarget(flags <= CameraClearFlags.Depth, flags == CameraClearFlags.Color,
            flags == CameraClearFlags.Color ? m_camera.backgroundColor.linear : Color.clear);
        buffer.BeginSample(bufferName);
        ExecuteBuffer();
    }

    /// <summary>
    /// 绘制可见物体
    /// 绘制顺序 不透明物体->天空盒->透明物体？？？
    /// </summary>
    private void DrawVisiableGeometry() {
        //设置绘制顺序和指定的相机
        var sortingSettings = new SortingSettings(m_camera){criteria = SortingCriteria.CommonOpaque};
        //设置渲染的Shader Pass和排序模式
        var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings);
        //设置哪些类型的渲染队列可以被绘制
        //1,只绘制不透明物体
        var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
        //绘制几何体
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
        //2,绘制天空盒
        context.DrawSkybox(m_camera);
        //3，绘制透明物体
        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
    }

    /// <summary>
    /// 提交缓冲区命令
    /// </summary>
    private void Submit() {
        buffer.EndSample(bufferName);
        ExecuteBuffer();
        //提交缓冲区命令
        context.Submit();
    }
    /// <summary>
    /// 执行命令缓冲区命令
    /// </summary>
    private void ExecuteBuffer() {
        context.ExecuteCommandBuffer(buffer);//执行缓冲区命令
        buffer.Clear();
    }
    
}

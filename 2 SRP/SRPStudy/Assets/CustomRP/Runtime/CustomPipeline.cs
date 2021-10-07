using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomPipeline : RenderPipeline
{
    CameraRender render = new CameraRender();
    /// <summary>
    /// 每一帧进行渲染，SRP入口
    /// </summary>
    /// <param name="context">ScriptableRenderContext和CommandBuffer是用于渲染的最底层的两个接口</param>
    /// <param name="cameras">参与这一帧渲染的所有相机</param>
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (var camera in cameras) {
            render.Render(context, camera);
        }
    }
}

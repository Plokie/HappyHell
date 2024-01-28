﻿using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PostProcessOutlineRenderer), PostProcessEvent.BeforeStack, "Roystan/Post Process Outline")]
public sealed class PostProcessOutline : PostProcessEffectSettings
{
    public IntParameter scale = new IntParameter { value = 1 };
    public FloatParameter depthThreshold = new FloatParameter { value = 1.5f };
    [Range(0, 1)]
    public FloatParameter normalThreshold = new FloatParameter { value = 0.4f };
    [Range(0, 1)]
    public FloatParameter depthNormalThreshold = new FloatParameter { value = 0.5f };
    public FloatParameter depthNormalThresholdScale = new FloatParameter { value = 7 };
    public FloatParameter darkLuminanceOffset = new FloatParameter { value = 0.8f };
    public ColorParameter edgeColor = new ColorParameter { value = Color.white };
    public ColorParameter backgroundColor = new ColorParameter { value = Color.black };
    public ColorParameter darkEdgeColor = new ColorParameter { value = Color.black };
    public ColorParameter darkBackgroundColor = new ColorParameter { value = Color.white };
    public ColorParameter skyColor = new ColorParameter { value = Color.white };
    [Range(0, 1)]
    public IntParameter lightMode = new IntParameter { value = 0 };
}

public sealed class PostProcessOutlineRenderer : PostProcessEffectRenderer<PostProcessOutline>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Roystan/Outline Post Process"));
        sheet.properties.SetFloat("_Scale", settings.scale);
        sheet.properties.SetFloat("_DepthThreshold", settings.depthThreshold);
        sheet.properties.SetFloat("_NormalThreshold", settings.normalThreshold);

        sheet.properties.SetColor("_SkyColor", settings.skyColor);

        sheet.properties.SetFloat("_DepthNormalThreshold", settings.depthNormalThreshold);
        sheet.properties.SetFloat("_DepthNormalThresholdScale", settings.depthNormalThresholdScale);

        sheet.properties.SetFloat("_LuminanceDarkOffset", settings.darkLuminanceOffset);

        sheet.properties.SetColor("_Color", settings.edgeColor);
        sheet.properties.SetColor("_BackgroundColor", settings.backgroundColor);

        sheet.properties.SetColor("_DarkEdgeColor", settings.darkEdgeColor);
        sheet.properties.SetColor("_DarkBackgroundColor", settings.darkBackgroundColor);


        sheet.properties.SetInt("_Invert", settings.lightMode);

        Matrix4x4 clipToView = GL.GetGPUProjectionMatrix(context.camera.projectionMatrix, true).inverse;
        sheet.properties.SetMatrix("_ClipToView", clipToView);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
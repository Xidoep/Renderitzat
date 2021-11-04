using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Renderitzat : MonoBehaviour
{
    const string KEY_GUARDAT_QUALITAT = "QualitatGrafica";

    public Guardat guardat;

    public UniversalRenderPipelineAsset baixa;
    public UniversalRenderPipelineAsset mitja;
    public UniversalRenderPipelineAsset alta;

    public void Qualitat(int qualitat)
    {
        switch (qualitat)
        {
            case 1:
                GraphicsSettings.renderPipelineAsset = baixa;
                break;
            case 2:
                GraphicsSettings.renderPipelineAsset = mitja;
                break;
            case 3:
                GraphicsSettings.renderPipelineAsset = alta;
                break;
        }
        guardat.Set(KEY_GUARDAT_QUALITAT, qualitat);
    }

    public void AugmentarQualitat()
    {
        if(GraphicsSettings.renderPipelineAsset != alta)
        {
            if(GraphicsSettings.renderPipelineAsset = baixa) GraphicsSettings.renderPipelineAsset = mitja;
            else if(GraphicsSettings.renderPipelineAsset = mitja) GraphicsSettings.renderPipelineAsset = alta;
        }
    }
    public void DisminueixQualitat()
    {
        if (GraphicsSettings.renderPipelineAsset != baixa)
        {
            if (GraphicsSettings.renderPipelineAsset = alta) GraphicsSettings.renderPipelineAsset = mitja;
            else if (GraphicsSettings.renderPipelineAsset = mitja) GraphicsSettings.renderPipelineAsset = baixa;
        }
    }

    public void CanviarRenderer(Camara cam, int index)
    {
        var x = cam.GetComponent<UniversalAdditionalCameraData>();
        x.SetRenderer(index);
    }
}

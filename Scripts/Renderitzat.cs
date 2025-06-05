using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Renderitzat : MonoBehaviour
{
    const string KEY_GUARDAT_QUALITAT = "QualitatGrafica";

    //public Guardat guardat;
    [SerializeField] SavableVariable<int> qualitat;

    public UniversalRenderPipelineAsset baixa;
    public UniversalRenderPipelineAsset mitja;
    public UniversalRenderPipelineAsset alta;

    public void Qualitat(int qualitat)
    {
        switch (qualitat)
        {
            case 1:
                GraphicsSettings.defaultRenderPipeline = baixa;
                break;
            case 2:
                GraphicsSettings.defaultRenderPipeline = mitja;
                break;
            case 3:
                GraphicsSettings.defaultRenderPipeline = alta;
                break;
        }
        this.qualitat.Valor = qualitat;
        //guardat.SetLocal(KEY_GUARDAT_QUALITAT, qualitat);
    }

    public void AugmentarQualitat()
    {
        if(GraphicsSettings.defaultRenderPipeline != alta)
        {
            if(GraphicsSettings.defaultRenderPipeline = baixa) GraphicsSettings.defaultRenderPipeline = mitja;
            else if(GraphicsSettings.defaultRenderPipeline = mitja) GraphicsSettings.defaultRenderPipeline = alta;
        }
    }
    public void DisminueixQualitat()
    {
        if (GraphicsSettings.defaultRenderPipeline != baixa)
        {
            if (GraphicsSettings.defaultRenderPipeline = alta) GraphicsSettings.defaultRenderPipeline = mitja;
            else if (GraphicsSettings.defaultRenderPipeline = mitja) GraphicsSettings.defaultRenderPipeline = baixa;
        }
    }

    public void CanviarRenderer(Camera cam, int index)
    {
        var x = cam.GetComponent<UniversalAdditionalCameraData>();
        x.SetRenderer(index);
    }

    private void OnValidate()
    {
        qualitat = new SavableVariable<int>(KEY_GUARDAT_QUALITAT, Guardat.Direccio.Local, 3);
        //guardat = XS_Utils.XS_Editor.LoadGuardat<Guardat>();
    }
}

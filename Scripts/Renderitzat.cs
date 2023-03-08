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
                GraphicsSettings.renderPipelineAsset = baixa;
                break;
            case 2:
                GraphicsSettings.renderPipelineAsset = mitja;
                break;
            case 3:
                GraphicsSettings.renderPipelineAsset = alta;
                break;
        }
        this.qualitat.Valor = qualitat;
        //guardat.SetLocal(KEY_GUARDAT_QUALITAT, qualitat);
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

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlitMaterialFeature : ScriptableRendererFeature
{
    class RenderPass : ScriptableRenderPass
    {

        private string profilingName;
        private Material material;
        private int materialPassIndex;
        private RenderTargetIdentifier sourceID;
        //private RenderTargetHandle tempTextureHandle;
        private RTHandle tempTextureHandle;

        public RenderPass(string profilingName, Material material, int passIndex) : base()
        {
            this.profilingName = profilingName;
            this.material = material;
            this.materialPassIndex = passIndex;
            //tempTextureHandle.Init("_TempBlitMaterialTexture");
            tempTextureHandle = RTHandles.Alloc(Vector2.one, depthBufferBits: DepthBits.Depth32, dimension: TextureDimension.Tex2D, name: "_TempBlitMaterialTexture");
        }

        public void SetSource(RenderTargetIdentifier source)
        {
            this.sourceID = source;
        }

        [System.Obsolete]
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilingName);

            RenderTextureDescriptor cameraTextureDesc = renderingData.cameraData.cameraTargetDescriptor;
            cameraTextureDesc.depthBufferBits = 0;

            cmd.GetTemporaryRT(tempTextureHandle.GetInstanceID(), cameraTextureDesc, FilterMode.Bilinear);
            Blit(cmd, sourceID, tempTextureHandle.nameID, material, materialPassIndex);
            Blit(cmd, tempTextureHandle.nameID, sourceID);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTextureHandle.GetInstanceID());
        }
    }

    [System.Serializable]
    public class Settings
    {
        public Material material;
        public int materialPassIndex = -1; // -1 means render all passes
        public RenderPassEvent renderEvent = RenderPassEvent.AfterRenderingOpaques;
    }

    [SerializeField]
    private Settings settings = new Settings();

    private RenderPass renderPass;

    public Material Material
    {
        get => settings.material;
    }

    public override void Create()
    {
        this.renderPass = new RenderPass(name, settings.material, settings.materialPassIndex);
        renderPass.renderPassEvent = settings.renderEvent;
    }

    [System.Obsolete]
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderPass.SetSource(renderer.cameraColorTargetHandle);
        renderer.EnqueuePass(renderPass);
    }
}

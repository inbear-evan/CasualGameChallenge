using UnityEngine;

namespace Yellotail.PostProcess
{
    [ExecuteInEditMode]
    public class BWEffect : MonoBehaviour
    {
        public float intensity;
        private Material material;

        // Creates a private material used to the effect
        void Awake()
        {
            this.material = new Material(Shader.Find("Hidden/BWDiffuse"));
        }

        // Postprocess the image
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (this.intensity == 0)
            {
                Graphics.Blit(source, destination);
                return;
            }

            this.material.SetFloat("_bwBlend", this.intensity);
            Graphics.Blit(source, destination, this.material);
        }
    }
}

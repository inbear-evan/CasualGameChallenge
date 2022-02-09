using UnityEngine;

namespace Yellotail.PostProcess
{
    [ExecuteInEditMode]
    public class Grayscale : MonoBehaviour
    {
        private Material material;

        // Creates a private material used to the effect
        void Awake()
        {
            this.material = new Material(Shader.Find("Hidden/GrayScale"));
        }

        // Postprocess the image
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, this.material);
        }
    }
}

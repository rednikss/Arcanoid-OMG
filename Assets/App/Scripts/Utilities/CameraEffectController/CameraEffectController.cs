
using UnityEngine;

namespace App.Scripts.Utilities.CameraEffectController
{
    public class CameraEffectController : MonoBehaviour
    {
        [SerializeField] private Material shaderMaterial;

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, shaderMaterial);
        }

    }
}
using System;
using UnityEngine;

namespace UnityStandardAssets.Water
{
    public enum WaterQuality
    {
        High = 2,
        Medium = 1,
        Low = 0,
    }

    [ExecuteInEditMode]
    public class WaterBase : MonoBehaviour
    {
        public Material sharedMaterial;
        public WaterQuality waterQuality = WaterQuality.High;
        public bool edgeBlend = true;

        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (sharedMaterial)
            {
                UpdateShader();
            }
        }

        public void UpdateShader()
        {
            if (waterQuality > WaterQuality.Medium)
            {
                sharedMaterial.shader.maximumLOD = 501;
            }
            else if (waterQuality > WaterQuality.Low)
            {
                sharedMaterial.shader.maximumLOD = 301;
            }
            else
            {
                sharedMaterial.shader.maximumLOD = 201;
            }

            // If the system does not support depth textures (ie. NaCl), turn off edge bleeding,
            // as the shader will render everything as transparent if the depth texture is not valid.
            if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
            {
                edgeBlend = false;
            }

            if(_camera)
            {
                if (edgeBlend)
                {
                    Shader.EnableKeyword("WATER_EDGEBLEND_ON");
                    Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
                    // just to make sure (some peeps might forget to add a water tile to the patches)
                    if (_camera)
                    {
                        _camera.depthTextureMode |= DepthTextureMode.Depth;
                    }
                }
                else
                {
                    Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
                    Shader.DisableKeyword("WATER_EDGEBLEND_ON");
                }
            }
        }


        public void WaterTileBeingRendered(Transform tr, Camera currentCam)
        {
            if (currentCam && edgeBlend)
            {
                currentCam.depthTextureMode |= DepthTextureMode.Depth;
            }
        }
    }
}
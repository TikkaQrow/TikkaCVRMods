using UnityEngine;

namespace VRCLightVolumes
{
    public class LightVolumeTVGI : MonoBehaviour
    {
        [Tooltip("Render Texture used by your video player. Can be just a static texture if you want it to be. Make sure that Enable Mip Maps and Auto Generate Mip Maps are Enabled in the texture’s import settings.")]
        public Texture TargetRenderTexture;

        [Tooltip("Enables smoothing algorithm that tries to smooth out flickering that is usually a problem. Recommended to always be turned on.")]
        public bool AntiFlickering = true;

        [Space]
        [Tooltip("List of the Light Volumes that should be affected by the Light Volume TVGI script.")]
        public LightVolumeInstance[] TargetLightVolumes;

        [Tooltip("List of the Point Light Volumes that should be affected by the Light Volume TVGI script. Usually you don't need it at all.")]
        public PointLightVolumeInstance[] TargetPointLightVolumes;

        private Color _prevColor;
        private float _timePrev;
        private RenderTexture _downsampledTex;

        private void Start()
        {
            _timePrev = Time.time;
            _prevColor = Color.black;

            _downsampledTex = new RenderTexture(64, 32, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear)
            {
                useMipMap = true,
                autoGenerateMips = true
            };
            _downsampledTex.Create();
        }

        private void OnDestroy()
        {
            if (_downsampledTex != null)
            {
                _downsampledTex.Release();
                Destroy(_downsampledTex);
                _downsampledTex = null;
            }
        }

        void Update()
        {
            if (TargetRenderTexture == null || _downsampledTex == null) return;

            Graphics.Blit(TargetRenderTexture, _downsampledTex);

            // read the last mip (most downsampled) for stable average color
            UnityEngine.Rendering.AsyncGPUReadback.Request(
                _downsampledTex,
                _downsampledTex.mipmapCount - 1,
                OnAsyncGpuReadbackComplete
            );
        }

        public void OnAsyncGpuReadbackComplete(UnityEngine.Rendering.AsyncGPUReadbackRequest request)
        {
            if (request.hasError || !request.done) return;

            var data = request.GetData<Color32>();   // no need to Dispose() this
            if (!data.IsCreated || data.Length == 0) return;

            // just use the first pixel from the most-downsampled mip
            SetColor(data[0]);
        }

        private void SetColor(Color32 sample)
        {
            // Custom delta time for the async stuff 
            float dTime = Time.time - _timePrev;
            _timePrev = Time.time;

            Color color = sample; // Current color

            if (AntiFlickering)
            {
                float diff = ColorDifference(color, _prevColor); // Difference between prev and current color
                float smoothing = dTime / Mathf.Lerp(0.25f, 1e-05f, Mathf.Pow(diff * 1.5f, 0.1f)); // speed depends on difference
                _prevColor = Color.Lerp(_prevColor, color, smoothing);
            }
            else
            {
                _prevColor = color;
            }

            // Applying all colors (null/length guards for safety)
            if (TargetLightVolumes != null)
            {
                for (int i = 0; i < TargetLightVolumes.Length; i++)
                {
                    if (TargetLightVolumes[i] != null)
                        TargetLightVolumes[i].Color = _prevColor;
                }
            }

            if (TargetPointLightVolumes != null)
            {
                for (int i = 0; i < TargetPointLightVolumes.Length; i++)
                {
                    if (TargetPointLightVolumes[i] != null)
                    {
                        TargetPointLightVolumes[i].Color = _prevColor;
                        TargetPointLightVolumes[i].IsRangeDirty = true;
                    }
                }
            }
        }

        private float ColorDifference(Color a, Color b)
        {
            float rmean = (a.r + b.r) * 0.5f;
            float r = a.r - b.r;
            float g = a.g - b.g;
            float bch = a.b - b.b;
            return Mathf.Sqrt((2f + rmean) * r * r + 4f * g * g + (3f - rmean) * bch * bch) / 3f;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Video;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(RawImage))]
    public class VideoViewer : MonoBehaviour
    {
        public VideoPlayer SourceVideoPlayer;

        public bool OverrideVideoSize = false;
        public int VideoWidth = 1280;
        public int VideoHeight = 720;
        public bool RenderAlphaChannel = false;

        [NonSerialized]
        private RawImage renderTarget;

        [NonSerialized]
        private RenderTexture renderTexture;

        private void Awake()
        {
            renderTarget = GetComponent<RawImage>();
            if (renderTarget.texture == null)
            {
                renderTarget.enabled = false;
            }
            if (SourceVideoPlayer == null)
            {
                SourceVideoPlayer = GetComponent<VideoPlayer>();
            }
            if (SourceVideoPlayer != null)
            {
                SourceVideoPlayer.prepareCompleted += SourceVideoPlayer_PrepareCompleted;
                SourceVideoPlayer.renderMode = VideoRenderMode.RenderTexture;
            }
        }

        private void OnEnable()
        {
            if (renderTarget.texture != null)
            {
                renderTarget.enabled = true;
            }
        }

        private void OnDisable()
        {
            renderTarget.enabled = false;
        }

        private void OnDestroy()
        {
            if (renderTexture != null)
            {
                renderTexture.Release();
            }
        }

        private void SourceVideoPlayer_PrepareCompleted(VideoPlayer source)
        {
            if (source.renderMode == VideoRenderMode.RenderTexture)
            {
                int sourceWidth = (int)source.width;
                int sourceHeight = (int)source.height;
                if (source.clip != null)
                {
                    sourceWidth = (int)source.clip.width;
                    sourceHeight = (int)source.clip.height;
                }
                int sourceDepth = 24;
                if (OverrideVideoSize)
                {
                    sourceWidth = VideoWidth;
                    sourceHeight = VideoHeight;
                }
                if (RenderAlphaChannel)
                {
                    sourceDepth = 32;
                }
                if (renderTexture == null || renderTexture.width != sourceWidth || renderTexture.height != sourceHeight || renderTexture.depth != sourceDepth)
                {
                    if (renderTexture != null)
                    {
                        renderTexture.Release();
                        renderTexture = null;
                    }
                    renderTexture = new RenderTexture(sourceWidth, sourceHeight, sourceDepth);
                    source.targetTexture = renderTexture;
                    renderTarget.texture = renderTexture;
                    renderTarget.enabled = enabled;
                }
            }
        }
    }
}

using System;

namespace UnityEngine
{
    public static class Texture2DUtility
    {
        public static Texture2D Flip(Texture2D source, bool flipX, bool flipY)
        {
            int targetWidth = source.width;
            int targetHeight = source.height;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] targetColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    targetColor[x + y * targetWidth] = sourceColor[(flipY ? targetWidth - x - 1 : x) + (flipX ? targetHeight - y - 1 : y) * targetWidth];
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(targetColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D Rotate90(Texture2D source, bool clockwise)
        {
            int targetWidth = source.height;
            int targetHeight = source.width;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] targetColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    targetColor[x + y * targetWidth] = sourceColor[(clockwise ? y + (targetWidth - x - 1) * targetHeight : (targetHeight - y - 1) + x * targetHeight)];
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(targetColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D Resize(Texture2D source, int targetWidth, int targetHeight)
        {
            int sourceWidth = source.width;
            int sourceHeight = source.height;
            float sourceAspect = (float)sourceWidth / sourceHeight;
            if (targetWidth <= 0)
            {
                targetWidth = Mathf.RoundToInt(targetHeight * sourceAspect);
            }
            else if (targetHeight <= 0)
            {
                targetHeight = Mathf.RoundToInt(targetWidth / sourceAspect);
            }

            float factorX = (float)targetWidth / sourceWidth;
            float factorY = (float)targetHeight / sourceHeight;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] targetColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    Vector2 p = new Vector2(Mathf.Clamp(x / factorX, 0, sourceWidth - 1), Mathf.Clamp(y / factorY, 0, sourceHeight - 1));
                    Color32 c11 = sourceColor[Mathf.FloorToInt(p.x) + sourceWidth * Mathf.FloorToInt(p.y)];
                    Color32 c12 = sourceColor[Mathf.FloorToInt(p.x) + sourceWidth * Mathf.CeilToInt(p.y)];
                    Color32 c21 = sourceColor[Mathf.CeilToInt(p.x) + sourceWidth * Mathf.FloorToInt(p.y)];
                    Color32 c22 = sourceColor[Mathf.CeilToInt(p.x) + sourceWidth * Mathf.CeilToInt(p.y)];
                    targetColor[x + y * targetWidth] = Color.Lerp(Color.Lerp(c11, c12, p.y), Color.Lerp(c21, c22, p.y), p.x);
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(targetColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D Crop(Texture2D source, int targetLeft, int targetTop, int targetWidth, int targetHeight)
        {
            int sourceWidth = source.width;
            int sourceHeight = source.height;
            if (targetLeft < 0 || targetLeft > sourceWidth)
            {
                targetLeft = 0;
            }
            if (targetTop < 0 || targetTop > sourceHeight)
            {
                targetTop = 0;
            }
            if (targetLeft + targetWidth > sourceWidth)
            {
                targetWidth = sourceWidth - targetLeft;
            }
            if (targetTop + targetHeight > sourceHeight)
            {
                targetHeight = sourceHeight - targetTop;
            }

            Color32[] sourceColor = source.GetPixels32();
            Color32[] targetColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    targetColor[x + y * targetWidth] = sourceColor[(x + targetLeft) + (y + (sourceHeight - targetHeight - targetTop)) * sourceWidth];
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(targetColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D Fill(Texture2D source, int fillLeft, int fillTop, int fillWidth, int fillHeight, Color fillColor)
        {
            int targetWidth = source.width;
            int targetHeight = source.height;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] targetColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    if (x >= fillLeft && x < fillLeft + fillWidth && y >= (targetHeight - fillHeight - fillTop) && y < targetHeight - fillTop)
                    {
                        targetColor[x + y * targetWidth] = fillColor;
                    }
                    else
                    {
                        targetColor[x + y * targetWidth] = sourceColor[x + y * targetWidth];
                    }
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(targetColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D AddBorder(Texture2D source, int borderLeft, int borderTop, int borderRight, int borderBottom, Color borderColor)
        {
            int sourceWidth = source.width;
            int sourceHeight = source.height;
            int targetWidth = sourceWidth + borderLeft + borderRight;
            int targetHeight = sourceHeight + borderTop + borderBottom;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] targetColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    if (x < borderLeft || y < borderBottom || x >= sourceWidth + borderLeft || y >= sourceHeight + borderBottom)
                    {
                        targetColor[x + y * targetWidth] = borderColor;
                    }
                    else
                    {
                        targetColor[x + y * targetWidth] = sourceColor[(x - borderLeft) + (y - borderBottom) * sourceWidth];
                    }
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(targetColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D CopyFromTexture(Texture texture)
        {
            if (texture != null)
            {
                RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height);
                RenderTexture activeTexture = RenderTexture.active;
                Graphics.Blit(texture, renderTexture);
                RenderTexture.active = renderTexture;
                Texture2D result = new Texture2D(texture.width, texture.height);
                result.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                result.Apply(true);
                RenderTexture.active = activeTexture;
                RenderTexture.ReleaseTemporary(renderTexture);
                return result;
            }
            return null;
        }
    }
}

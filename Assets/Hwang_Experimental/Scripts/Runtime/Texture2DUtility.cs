using System;
using System.IO;

namespace UnityEngine
{
    public static class Texture2DUtility
    {
        public static Texture2D Flip(Texture2D source, bool flipX, bool flipY)
        {
            int sourceWidth = source.width;
            int sourceHeight = source.height;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] resultColor = new Color32[sourceWidth * sourceHeight];
            for (int y = 0; y < sourceHeight; y++)
            {
                for (int x = 0; x < sourceWidth; x++)
                {
                    resultColor[x + y * sourceWidth] = sourceColor[(flipY ? sourceWidth - x - 1 : x) + (flipX ? sourceHeight - y - 1 : y) * sourceWidth];
                }
            }

            Texture2D result = new Texture2D(sourceWidth, sourceHeight);
            result.SetPixels32(resultColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D Rotate90(Texture2D source, bool clockwise)
        {
            int sourceWidth = source.height;
            int sourceHeight = source.width;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] resultColor = new Color32[sourceWidth * sourceHeight];
            for (int y = 0; y < sourceHeight; y++)
            {
                for (int x = 0; x < sourceWidth; x++)
                {
                    resultColor[x + y * sourceWidth] = sourceColor[(clockwise ? y + (sourceWidth - x - 1) * sourceHeight : (sourceHeight - y - 1) + x * sourceHeight)];
                }
            }

            Texture2D result = new Texture2D(sourceWidth, sourceHeight);
            result.SetPixels32(resultColor);
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
            Color32[] resultColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    Vector2 p = new Vector2(Mathf.Clamp(x / factorX, 0, sourceWidth - 1), Mathf.Clamp(y / factorY, 0, sourceHeight - 1));
                    Color32 c11 = sourceColor[Mathf.FloorToInt(p.x) + sourceWidth * Mathf.FloorToInt(p.y)];
                    Color32 c12 = sourceColor[Mathf.FloorToInt(p.x) + sourceWidth * Mathf.CeilToInt(p.y)];
                    Color32 c21 = sourceColor[Mathf.CeilToInt(p.x) + sourceWidth * Mathf.FloorToInt(p.y)];
                    Color32 c22 = sourceColor[Mathf.CeilToInt(p.x) + sourceWidth * Mathf.CeilToInt(p.y)];
                    resultColor[x + y * targetWidth] = Color.Lerp(Color.Lerp(c11, c12, p.y), Color.Lerp(c21, c22, p.y), p.x);
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(resultColor);
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
            Color32[] resultColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    resultColor[x + y * targetWidth] = sourceColor[(x + targetLeft) + (y + (sourceHeight - targetHeight - targetTop)) * sourceWidth];
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(resultColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D Fill(Texture2D source, int fillLeft, int fillTop, int fillWidth, int fillHeight, Color fillColor)
        {
            int sourceWidth = source.width;
            int sourceHeight = source.height;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] resultColor = new Color32[sourceWidth * sourceHeight];
            for (int y = 0; y < sourceHeight; y++)
            {
                for (int x = 0; x < sourceWidth; x++)
                {
                    if (x >= fillLeft && x < fillLeft + fillWidth && y >= (sourceHeight - fillHeight - fillTop) && y < sourceHeight - fillTop)
                    {
                        resultColor[x + y * sourceWidth] = fillColor;
                    }
                    else
                    {
                        resultColor[x + y * sourceWidth] = sourceColor[x + y * sourceWidth];
                    }
                }
            }

            Texture2D result = new Texture2D(sourceWidth, sourceHeight);
            result.SetPixels32(resultColor);
            result.Apply(true);
            return result;
        }

        public static Texture2D DrawTexture(Texture2D source, int drawLeft, int drawTop, Texture2D target)
        {
            int sourceWidth = source.width;
            int sourceHeight = source.height;
            int targetWidth = target.width;
            int targetHeight = target.height;
            Color32[] sourceColor = source.GetPixels32();
            Color32[] targetColor = target.GetPixels32();
            Color32[] resultColor = new Color32[sourceWidth * sourceHeight];
            Color32 pixel;
            for (int y = 0; y < sourceHeight; y++)
            {
                for (int x = 0; x < sourceWidth; x++)
                {
                    if (x >= drawLeft && x < drawLeft + targetWidth && y >= (sourceHeight - targetHeight - drawTop) && y < sourceHeight - drawTop)
                    {
                        pixel = targetColor[(x - drawLeft) + (y - (sourceHeight - targetHeight - drawTop)) * targetWidth];
                        resultColor[x + y * sourceWidth] = Color32.Lerp(sourceColor[x + y * sourceWidth], pixel, pixel.a);
                    }
                    else
                    {
                        resultColor[x + y * sourceWidth] = sourceColor[x + y * sourceWidth];
                    }
                }
            }

            Texture2D result = new Texture2D(sourceWidth, sourceHeight);
            result.SetPixels32(resultColor);
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
            Color32[] resultColor = new Color32[targetWidth * targetHeight];
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    if (x < borderLeft || y < borderBottom || x >= sourceWidth + borderLeft || y >= sourceHeight + borderBottom)
                    {
                        resultColor[x + y * targetWidth] = borderColor;
                    }
                    else
                    {
                        resultColor[x + y * targetWidth] = sourceColor[(x - borderLeft) + (y - borderBottom) * sourceWidth];
                    }
                }
            }

            Texture2D result = new Texture2D(targetWidth, targetHeight);
            result.SetPixels32(resultColor);
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
                Texture2D result = new Texture2D(renderTexture.width, renderTexture.height);
                result.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                result.Apply(true);
                RenderTexture.active = activeTexture;
                RenderTexture.ReleaseTemporary(renderTexture);
                return result;
            }
            return null;
        }

        public static Texture2D CaptureFromCamera(Camera camera = null)
        {
            if (camera == null)
            {
                camera = Camera.main;
            }
            if (camera != null)
            {
                RenderTexture renderTexture = RenderTexture.GetTemporary(camera.pixelWidth, camera.pixelHeight);
                RenderTexture targetTexture = camera.targetTexture;
                RenderTexture activeTexture = RenderTexture.active;
                camera.targetTexture = renderTexture;
                camera.Render();
                RenderTexture.active = renderTexture;
                Texture2D result = new Texture2D(renderTexture.width, renderTexture.height);
                result.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
                result.Apply(true);
                camera.targetTexture = targetTexture;
                RenderTexture.active = activeTexture;
                RenderTexture.ReleaseTemporary(renderTexture);
                return result;
            }
            return null;
        }

        public static Texture2D LoadFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = string.Format("{0}/image.jpg", Application.persistentDataPath);
            }
            string ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext))
            {
                path = string.Format("{0}.jpg", path);
            }
            string dir = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dir))
            {
                path = string.Format("{0}/{1}", Application.persistentDataPath, path);
            }
            try
            {
                Texture2D result = new Texture2D(2, 2);
                if (result.LoadImage(File.ReadAllBytes(path)))
                {
                    return result;
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static bool SaveToFile(Texture2D source, string path = null, int quality_JPG = 0)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = string.Format("{0}/image.jpg", Application.persistentDataPath);
            }
            string ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext))
            {
                path = string.Format("{0}.jpg", path);
            }
            string dir = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dir))
            {
                path = string.Format("{0}/{1}", Application.persistentDataPath, path);
                dir = Application.persistentDataPath;
            }
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (string.Compare(ext, ".png", true) == 0)
                {
                    File.WriteAllBytes(path, source.EncodeToPNG());
                }
#if UNITY_2018_3_OR_NEWER
                else if (string.Compare(ext, ".tga", true) == 0)
                {
                    File.WriteAllBytes(path, source.EncodeToTGA());
                }
#endif
                else if (quality_JPG == 0)
                {
                    File.WriteAllBytes(path, source.EncodeToJPG());
                }
                else
                {
                    File.WriteAllBytes(path, source.EncodeToJPG(quality_JPG));
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
    }
}

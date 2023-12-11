using System;
using System.Collections;
using UnityEngine;

namespace UnityEngine.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public sealed class SpriteAnimator : MonoBehaviour
    {
        [SerializeField]
        private int startIndex = -1;
        private int StartIndex
        {
            get
            {
                return startIndex;
            }
            set
            {
                SpriteIndex = value;
                startIndex = spriteIndex;
            }
        }

        public Sprite[] Sprites = new Sprite[0];

        public float StartDelay = 0f;
        public float Duration = 1f;
        public int LoopCount = -1;
        public bool PlayOnStart = true;
        public bool PlayOnEnable = false;
        public bool IgnoreTimeScale = true;

        [NonSerialized]
        private bool isPlaying = false;

        [NonSerialized]
        private float interval = 0f;

        [NonSerialized]
        private int numLooping = 0;

        [NonSerialized]
        private int spriteIndex = -1;
        public int SpriteIndex
        {
            get
            {
                return spriteIndex;
            }
            set
            {
                if (value >= -1 && value < Sprites.Length)
                {
                    spriteIndex = value;
                    UpdateSprite();
                }
            }
        }

        [NonSerialized]
        private Image _image;
        private Image image
        {
            get
            {
                if (_image == null)
                {
                    _image = GetComponent<Image>();
                }
                return _image;
            }
        }

        [NonSerialized]
        private Coroutine updatingRoutine;

        private void Start()
        {
            _image = GetComponent<Image>();

            if (PlayOnStart)
            {
                Play();
            }
        }

        private void OnEnable()
        {
            UpdateSprite();
            if (PlayOnEnable)
            {
                Play();
            }
        }

        private void OnDisable()
        {
            Stop();
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            if (isActiveAndEnabled && spriteIndex >= 0 && spriteIndex < Sprites.Length)
            {
                if (image != null)
                {
                    image.overrideSprite = Sprites[spriteIndex];
                }
            }
            else
            {
                if (image != null)
                {
                    image.overrideSprite = null;
                }
            }
        }

        private IEnumerator Updating()
        {
            if (IgnoreTimeScale)
            {
                yield return new WaitForSecondsRealtime(StartDelay);
            }
            else
            {
                yield return new WaitForSeconds(StartDelay);
            }

            while (isPlaying)
            {
                UpdateSprite();
                if (IgnoreTimeScale)
                {
                    yield return new WaitForSecondsRealtime(interval);
                }
                else
                {
                    yield return new WaitForSeconds(interval);
                }
                spriteIndex++;
                if (spriteIndex < 0 || spriteIndex >= Sprites.Length)
                {
                    spriteIndex = 0;
                }
                if (LoopCount >= 0)
                {
                    if (spriteIndex == startIndex + 1 || (spriteIndex == 0 && startIndex <= 0) || (spriteIndex == 0 && startIndex >= Sprites.Length - 1))
                    {
                        if (numLooping == LoopCount)
                        {
                            Stop();
                            yield break;
                        }
                        numLooping++;
                    }
                }
            }
        }

        public void Play()
        {
            if (!isPlaying)
            {
                spriteIndex = startIndex;
                UpdateSprite();
                interval = Duration / (Sprites.Length + 1);
                isPlaying = true;
                numLooping = 0;
                updatingRoutine = StartCoroutine(Updating());
            }
        }

        public void Stop()
        {
            isPlaying = false;
            numLooping = 0;
            if (updatingRoutine != null)
            {
                StopCoroutine(updatingRoutine);
            }
        }

#if UNITY_EDITOR
        [NonSerialized]
        private int spriteCount = 0;

        [NonSerialized]
        private float duration = 1f;

        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                if (duration != Duration)
                {
                    interval = Duration / (Sprites.Length + 1);
                    duration = Duration;
                }
            }
            else
            {
                if (spriteIndex != startIndex || spriteCount != Sprites.Length)
                {
                    spriteIndex = startIndex;
                    spriteCount = Sprites.Length;
                }
                UpdateSprite();
            }
        }

        private void Reset()
        {
            numLooping = 0;
            UpdateSprite();
        }
#endif
    }
}

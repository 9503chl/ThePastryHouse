using System.Collections;
using UnityEngine;

namespace UnityEngine.UI
{
    public sealed class TweenSize : UITweener
    {
        public Vector2 FromValue = Vector2.one;

        public Vector2 ToValue = Vector2.one;

        private Vector2 GetValue()
        {
            if (rectTransform != null)
            {
                return rectTransform.rect.size;
            }
            else
            {
                return transform.localScale;
            }
        }

        private void SetValue(Vector2 value)
        {
            if (rectTransform != null)
            {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value.x);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value.y);
            }
            else
            {
                transform.localScale = new Vector3(value.x, value.y, transform.localScale.z);
            }
        }

        protected override void UpdateProgress(float progress)
        {
            SetValue(Vector2.Lerp(FromValue, ToValue, progress));
        }

#if UNITY_EDITOR
        [ContextMenu("Swap 'From' And 'To'")]
        private void SwapValues()
        {
            Vector2 temp = FromValue;
            FromValue = ToValue;
            ToValue = temp;
        }

        private void Reset()
        {
            FromValue = GetValue();
            ToValue = GetValue();
        }
#endif

        public static TweenSize Create(GameObject target, Vector2 fromValue, Vector2 toValue, float duration, TweeningLoopType loopType = TweeningLoopType.Once, TweeningEaseType easeType = TweeningEaseType.Linear)
        {
            if (target != null)
            {
                TweenSize tweener = target.GetComponent<TweenSize>();
                if (tweener == null)
                {
                    tweener = target.AddComponent<TweenSize>();
                }
                else
                {
                    tweener.Stop();
                }
                tweener.FromValue = fromValue;
                tweener.ToValue = toValue;
                tweener.Duration = duration;
                tweener.LoopType = loopType;
                tweener.EaseType = easeType;
                tweener.enabled = true;
                return tweener;
            }
            return null;
        }

        public static TweenSize Play(GameObject target, Vector2 fromValue, Vector2 toValue, float duration, TweeningLoopType loopType = TweeningLoopType.Once, TweeningEaseType easeType = TweeningEaseType.Linear)
        {
            TweenSize tweener = Create(target, fromValue, toValue, duration, loopType, easeType);
            if (tweener != null)
            {
                tweener.PlayForward();
            }
            return tweener;
        }
    }
}

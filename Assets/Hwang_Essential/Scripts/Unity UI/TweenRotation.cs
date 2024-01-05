using System.Collections;
using UnityEngine;

namespace UnityEngine.UI
{
    public sealed class TweenRotation : UITweener
    {
        public Vector3 FromValue = Vector3.zero;

        public Vector3 ToValue = Vector3.zero;

        private Vector3 GetValue()
        {
            return transform.localEulerAngles;
        }

        private void SetValue(Vector3 value)
        {
            transform.localEulerAngles = value;
        }

        protected override void UpdateProgress(float progress)
        {
            SetValue(Vector3.Lerp(FromValue, ToValue, progress));
        }

#if UNITY_EDITOR
        [ContextMenu("Swap 'From' And 'To'")]
        private void SwapTwoValues()
        {
            Vector3 temp = FromValue;
            FromValue = ToValue;
            ToValue = temp;
        }

        private void Reset()
        {
            FromValue = GetValue();
            ToValue = GetValue();
        }
#endif

        public static TweenRotation Create(GameObject target, Vector3 fromValue, Vector3 toValue, float duration, TweeningLoopType loopType = TweeningLoopType.Once, TweeningEaseType easeType = TweeningEaseType.Linear)
        {
            if (target != null)
            {
                TweenRotation tweener = target.GetComponent<TweenRotation>();
                if (tweener == null)
                {
                    tweener = target.AddComponent<TweenRotation>();
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

        public static TweenRotation Play(GameObject target, Vector3 fromValue, Vector3 toValue, float duration, TweeningLoopType loopType = TweeningLoopType.Once, TweeningEaseType easeType = TweeningEaseType.Linear)
        {
            TweenRotation tweener = Create(target, fromValue, toValue, duration, loopType, easeType);
            if (tweener != null)
            {
                tweener.PlayForward();
            }
            return tweener;
        }
    }
}

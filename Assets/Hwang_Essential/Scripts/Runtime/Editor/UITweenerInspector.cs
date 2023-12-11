using UnityEditor;
using UnityEngine;

namespace UnityEngine.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UITweener), true)]
    public class UITweenerInspector : Editor
    {
        private SerializedProperty fromValueProperty;
        private SerializedProperty toValueProperty;
        private SerializedProperty useRigidbodyProperty;

        private SerializedProperty loopTypeProperty;
        private SerializedProperty easeTypeProperty;
        private SerializedProperty animationCurveProperty;
        private SerializedProperty startDelayProperty;
        private SerializedProperty durationProperty;
        private SerializedProperty initOnPlayProperty;
        private SerializedProperty playOnStartProperty;
        private SerializedProperty playOnEnableProperty;
        private SerializedProperty stopAfterLoopProperty;
        private SerializedProperty ignoreTimeScaleProperty;

        private SerializedProperty onFinishProperty;

        public virtual void OnEnable()
        {
            fromValueProperty = serializedObject.FindProperty("FromValue");
            toValueProperty = serializedObject.FindProperty("ToValue");
            useRigidbodyProperty = serializedObject.FindProperty("UseRigidbody");

            loopTypeProperty = serializedObject.FindProperty("LoopType");
            easeTypeProperty = serializedObject.FindProperty("EaseType");
            animationCurveProperty = serializedObject.FindProperty("CustomAnimationCurve");
            startDelayProperty = serializedObject.FindProperty("Delay");
            durationProperty = serializedObject.FindProperty("Duration");
            initOnPlayProperty = serializedObject.FindProperty("InitOnPlay");
            playOnStartProperty = serializedObject.FindProperty("PlayOnStart");
            playOnEnableProperty = serializedObject.FindProperty("PlayOnEnable");
            stopAfterLoopProperty = serializedObject.FindProperty("StopAfterLoop");
            ignoreTimeScaleProperty = serializedObject.FindProperty("IgnoreTimeScale");

            onFinishProperty = serializedObject.FindProperty("onFinish");
        }

        public override void OnInspectorGUI()
        {
            if (target == null)
            {
                DrawDefaultInspector();
                return;
            }

            serializedObject.Update();

            if (fromValueProperty != null)
            {
                EditorGUILayout.PropertyField(fromValueProperty, new GUIContent("From"));
            }
            if (toValueProperty != null)
            {
                EditorGUILayout.PropertyField(toValueProperty, new GUIContent("To"));
            }
            if (useRigidbodyProperty != null)
            {
                EditorGUILayout.PropertyField(useRigidbodyProperty);
            }

            EditorGUILayout.PropertyField(loopTypeProperty);
            EditorGUILayout.PropertyField(easeTypeProperty);
            if (easeTypeProperty.intValue == (int)TweeningEaseType.Custom)
            {
                EditorGUILayout.PropertyField(animationCurveProperty, new GUIContent("Animation Curve"), GUILayout.MinHeight(50));
            }
            EditorGUILayout.PropertyField(startDelayProperty, new GUIContent("Start Delay"));
            EditorGUILayout.PropertyField(durationProperty);
            EditorGUILayout.PropertyField(initOnPlayProperty);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(playOnStartProperty);
            if (EditorGUI.EndChangeCheck())
            {
                if (playOnStartProperty.boolValue)
                {
                    playOnEnableProperty.boolValue = false;
                }
            }
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(playOnEnableProperty);
            if (EditorGUI.EndChangeCheck())
            {
                if (playOnEnableProperty.boolValue)
                {
                    playOnStartProperty.boolValue = false;
                }
            }
            if (loopTypeProperty.intValue != (int)TweeningLoopType.Once)
            {
                EditorGUILayout.PropertyField(stopAfterLoopProperty);
            }
            EditorGUILayout.PropertyField(ignoreTimeScaleProperty);
            EditorGUILayout.PropertyField(onFinishProperty);

            if (EditorApplication.isPlaying)
            {
                EditorGUILayout.Space();
                bool isEnabled = false;
                foreach (UITweener tweener in targets)
                {
                    if (tweener != null && tweener.isActiveAndEnabled)
                    {
                        isEnabled = true;
                        break;
                    }
                }
                EditorGUI.BeginDisabledGroup(!isEnabled);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Play forward"))
                {
                    foreach (UITweener tweener in targets)
                    {
                        if (tweener != null && tweener.isActiveAndEnabled)
                        {
                            tweener.PlayForward();
                        }
                    }
                }
                if (GUILayout.Button("Play reverse"))
                {
                    foreach (UITweener tweener in targets)
                    {
                        if (tweener != null && tweener.isActiveAndEnabled)
                        {
                            tweener.PlayReverse();
                        }
                    }
                }
                if (GUILayout.Button("Stop"))
                {
                    foreach (UITweener tweener in targets)
                    {
                        if (tweener != null && tweener.isActiveAndEnabled)
                        {
                            tweener.Stop();
                        }
                    }
                }
                if (GUILayout.Button("Finish"))
                {
                    foreach (UITweener tweener in targets)
                    {
                        if (tweener != null && tweener.isActiveAndEnabled)
                        {
                            tweener.Finish();
                        }
                    }
                }
                GUILayout.EndHorizontal();
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

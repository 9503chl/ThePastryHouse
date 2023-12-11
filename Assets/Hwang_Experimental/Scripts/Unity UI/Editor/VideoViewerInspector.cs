using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace UnityEngine.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(VideoViewer), true)]
    public class VideoViewerInspector : Editor
    {
        private SerializedProperty sourceVideoPlayerProperty;
        private SerializedProperty overrideVideoSizeProperty;
        private SerializedProperty videoWidthProperty;
        private SerializedProperty videoHeightProperty;
        private SerializedProperty renderAlphaChannelProperty;

        public virtual void OnEnable()
        {
            sourceVideoPlayerProperty = serializedObject.FindProperty("SourceVideoPlayer");
            overrideVideoSizeProperty = serializedObject.FindProperty("OverrideVideoSize");
            videoWidthProperty = serializedObject.FindProperty("VideoWidth");
            videoHeightProperty = serializedObject.FindProperty("VideoHeight");
            renderAlphaChannelProperty = serializedObject.FindProperty("RenderAlphaChannel");
            VideoViewer videoViewer = target as VideoViewer;
            if (videoViewer.SourceVideoPlayer == null || (videoViewer.SourceVideoPlayer.targetTexture == null && videoViewer.GetComponent<RawImage>().texture == null))
            {
                videoViewer.GetComponent<RawImage>().enabled = false;
            }
        }

        public override void OnInspectorGUI()
        {
            if (target == null)
            {
                DrawDefaultInspector();
                return;
            }

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(sourceVideoPlayerProperty);
            if (EditorGUI.EndChangeCheck())
            {
                VideoViewer videoViewer = target as VideoViewer;
                if (sourceVideoPlayerProperty.objectReferenceValue != null)
                {
                    VideoPlayer videoPlayer = sourceVideoPlayerProperty.objectReferenceValue as VideoPlayer;
                    videoViewer.GetComponent<RawImage>().texture = videoPlayer.targetTexture;
                    videoViewer.GetComponent<RawImage>().enabled = (videoPlayer.targetTexture != null) || (videoViewer.GetComponent<RawImage>().texture != null);
                }
            }
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlaying);
            EditorGUILayout.PropertyField(overrideVideoSizeProperty);
            if (overrideVideoSizeProperty.boolValue)
            {
                EditorGUILayout.PropertyField(videoWidthProperty);
                EditorGUILayout.PropertyField(videoHeightProperty);
            }
            EditorGUILayout.PropertyField(renderAlphaChannelProperty);
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEngine.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DropdownButtonGroup), true)]
    public class DropdownButtonGroupInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw default inspector when "Script" is missing or null
            if (target == null)
            {
                base.OnInspectorGUI();
                return;
            }

            // Update SerializedObject and get first property
            serializedObject.Update();
            SerializedProperty property = serializedObject.GetIterator();
            property.NextVisible(true);

            // Draw "Script" property at first
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(property);
            EditorGUI.EndDisabledGroup();

            // Draw other properties
            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, true);

                // Add a button below of "Buttons" property when expanded
                if (string.Compare(property.name, "Buttons") == 0 && property.isExpanded)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    if (GUILayout.Button("Find buttons in children"))
                    {
                        // Find Button components in children for all selected DropdownButtonGroup
                        foreach (DropdownButtonGroup group in targets)
                        {
                            // Exclude from result if Button is on DropdownButtonGroup itself or dropdown button
                            List<Button> buttons = new List<Button>(group.gameObject.GetComponentsInChildren<Button>(true));
                            foreach (Button button in buttons)
                            {
                                if (button.gameObject == group.gameObject || button == group.DropdownButton)
                                {
                                    buttons.Remove(button);
                                    break;
                                }
                            }
                            group.Buttons = buttons.ToArray();
                        }
                    }
                    GUILayout.Space(20);
                    EditorGUILayout.EndHorizontal();
                }
            }

            // Apply modified properties in inspector
            serializedObject.ApplyModifiedProperties();
        }
    }
}

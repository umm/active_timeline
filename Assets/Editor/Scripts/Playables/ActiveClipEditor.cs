using ActiveTimeline.Enumerate;
using UnityEditor;
using UnityEditorInternal;

namespace ActiveTimeline.Playables
{
    [CustomEditor(typeof(LabelClip))]
    public class LabelClipEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("label"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("template"));
            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(GoToClip))]
    public class GoToClipEditor : Editor
    {
        private ReorderableList ReorderableList { get; set; }

        private void OnEnable()
        {
            var property = serializedObject.FindProperty("template.predicateList");
            ReorderableList = new ReorderableList(serializedObject, property);
            ReorderableList.elementHeight = 4 * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) + EditorGUIUtility.singleLineHeight / 4.0f;
            ReorderableList.drawHeaderCallback =
                rect =>
                    EditorGUI
                        .LabelField(rect, "Predicate List")
                ;
            ReorderableList.drawElementCallback =
                (rect, index, active, focused) =>
                    EditorGUI
                        .PropertyField(rect, ReorderableList.serializedProperty.GetArrayElementAtIndex(index))
                ;
            ReorderableList.onAddCallback =
                list =>
                {
                    if (property.arraySize == 0)
                    {
                        property.arraySize++;
                        list.index = property.arraySize - 1;
                    }
                    else
                    {
                        property.arraySize++;
                        list.index = property.arraySize - 1;
                        var element = property.GetArrayElementAtIndex(list.index);
                        element.FindPropertyRelative("condition.exposedName").stringValue = string.Empty;
                        element.FindPropertyRelative("checkEveryFrame").boolValue = false;
                        element.FindPropertyRelative("targetType").intValue = (int) TargetType.None;
                    }
                };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("label"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("template.defaultBehaviour"));
            ReorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}

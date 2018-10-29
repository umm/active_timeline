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

    [CustomEditor(typeof(JumpClip))]
    public class JumpClipEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("label"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("template"));
            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(WaitClip))]
    public class WaitClipEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("label"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("template"));
            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(LoopClip))]
    public class LoopClipEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("label"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("template"));
            serializedObject.ApplyModifiedProperties();
        }
    }

    [CustomEditor(typeof(SwitchClip))]
    public class SwitchClipEditor : Editor
    {
        private ReorderableList ReorderableList { get; set; }

        private void OnEnable()
        {
            ReorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("template.predicateList"));
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
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("label"));
            ReorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
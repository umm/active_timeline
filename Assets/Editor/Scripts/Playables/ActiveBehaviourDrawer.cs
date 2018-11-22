using UnityEditor;
using UnityEngine;

namespace ActiveTimeline.Playables
{
    public abstract class ActiveBehaviourDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
        }
    }

    [CustomPropertyDrawer(typeof(LabelBehaviour))]
    public class LabelBehaviourDrawer : ActiveBehaviourDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Do nothing.
        }
    }

    [CustomPropertyDrawer(typeof(JumpBehaviour))]
    public class JumpBehaviourDrawer : ActiveBehaviourDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, "Predicate to Jump");
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("predicateToJump"));
            EditorGUI.indentLevel--;
        }
    }

    [CustomPropertyDrawer(typeof(LoopBehaviour))]
    public class LoopBehaviourDrawer : ActiveBehaviourDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, "Predicate to Break");
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("predicateToBreak"));
            EditorGUI.indentLevel--;
        }
    }

    [CustomPropertyDrawer(typeof(WaitBehaviour))]
    public class WaitBehaviourDrawer : ActiveBehaviourDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.LabelField(position, "Predicate to Resume");
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("predicateToResume"));
            EditorGUI.indentLevel--;
        }
    }

    [CustomPropertyDrawer(typeof(SwitchBehaviour))]
    public class SwitchBehaviourDrawer : ActiveBehaviourDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
        }
    }
}

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

    [CustomPropertyDrawer(typeof(GoToBehaviour))]
    public class GoToBehaviourDrawer : ActiveBehaviourDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
        }
    }
}

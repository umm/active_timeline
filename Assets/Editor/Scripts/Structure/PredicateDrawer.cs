using System;
using System.Linq;
using ActiveTimeline.Enumerate;
using UnityEditor;
using UnityEngine;

namespace ActiveTimeline.Structure
{
    [CustomPropertyDrawer(typeof(TransitionablePredicate))]
    public class TransitionablePredicateDrawer : DrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ResetLineCount();

            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var rect = new Rect(position) {height = EditorGUIUtility.singleLineHeight,};
                EditorGUI.PropertyField(rect, property.FindPropertyRelative("condition"), new GUIContent("Condition"));
                NewLine(ref rect);

                EditorGUI.PropertyField(rect, property.FindPropertyRelative("checkEveryFrame"), new GUIContent("Check Every Frame ?"));
                NewLine(ref rect);

                var targetTypeProperty = property.FindPropertyRelative("targetType");
                targetTypeProperty.intValue = (int) (TargetType) EditorGUI.EnumPopup(rect, "Type", (TargetType) targetTypeProperty.intValue);
                NewLine(ref rect);

                switch ((TargetType) targetTypeProperty.intValue)
                {
                    case TargetType.None:
                        break;
                    case TargetType.Time:
                        EditorGUI.PropertyField(rect, property.FindPropertyRelative("time"));
                        NewLine(ref rect);
                        break;
                    case TargetType.Frame:
                        EditorGUI.PropertyField(rect, property.FindPropertyRelative("frame"));
                        NewLine(ref rect);
                        break;
                    case TargetType.Clip:
                        var targetLabel = property.FindPropertyRelative("label").stringValue;
                        var clipLabels = CollectClipLabels(property).ToArray();
                        var selectedIndex = clipLabels.Any(x => x == targetLabel) ? clipLabels.Select((v, i) => new {v, i}).First(x => x.v == targetLabel).i : -1;
                        var index = EditorGUI.Popup(
                            rect,
                            new GUIContent("Target"),
                            selectedIndex,
                            clipLabels.Select(x => new GUIContent(x)).ToArray()
                        );
                        if (index >= 0 && index < clipLabels.Length)
                        {
                            property.FindPropertyRelative("label").stringValue = clipLabels[index];
                        }

                        NewLine(ref rect);
                        break;
                    case TargetType.First:
                        break;
                    case TargetType.Last:
                        break;
                    case TargetType.PlayableDirector:
                        EditorGUI.PropertyField(rect, property.FindPropertyRelative("playableDirector"));
                        NewLine(ref rect);
                        break;
                    case TargetType.Event:
                        EditorGUI.PropertyField(rect, property.FindPropertyRelative("eventTrigger"));
                        NewLine(ref rect);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    [CustomPropertyDrawer(typeof(Predicate))]
    public class PredicateDrawer : DrawerBase
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ResetLineCount();
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                var rect = new Rect(position) {height = EditorGUIUtility.singleLineHeight,};
                EditorGUI.PropertyField(rect, property.FindPropertyRelative("condition"), new GUIContent("Condition"));
                NewLine(ref rect);

                EditorGUI.PropertyField(rect, property.FindPropertyRelative("checkEveryFrame"), new GUIContent("Check Every Frame ?"));
                NewLine(ref rect);
            }
        }
    }
}

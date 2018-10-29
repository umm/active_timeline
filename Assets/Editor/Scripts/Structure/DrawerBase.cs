using System.Collections.Generic;
using ActiveTimeline.Playables;
using UnityEditor;
using UnityEngine;

namespace ActiveTimeline.Structure
{
    public class DrawerBase : PropertyDrawer
    {
        private int LineCount { get; set; }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return LineCount * EditorGUIUtility.singleLineHeight;
        }

        protected static IEnumerable<string> CollectClipLabels(SerializedProperty property)
        {
            var trackHolder = property.serializedObject.targetObject as ITrackHolder;
            if (trackHolder == null)
            {
                return new string[0];
            }

            var activeTrack = trackHolder.Track as ActiveTrack;
            if (activeTrack == null)
            {
                return new string[0];
            }

            return activeTrack.MarkerMap.Keys;
        }

        protected void ResetLineCount()
        {
            LineCount = 0;
        }

        protected void NewLine(ref Rect rect)
        {
            rect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            LineCount++;
        }
    }
}
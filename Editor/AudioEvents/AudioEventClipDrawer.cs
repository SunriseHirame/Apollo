using UnityEditor;
using UnityEngine;

namespace Hirame.Apollo.Editor
{
    [CustomPropertyDrawer (typeof (AudioEventClip))]
    public class AudioEventClipDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            // Get properties
            var clipProp = property.FindPropertyRelative ("clip");
            var volumeProp = property.FindPropertyRelative ("volumeRange");
            var pitchProp = property.FindPropertyRelative ("pitchRange");

            // Find the name of the Attached AudioClip
            var clipName = clipProp.objectReferenceValue ? clipProp.objectReferenceValue.name : "None (Audio Clip)";

            // See if we are part of an array and find index.
            var propPath = property.propertyPath;
            var isArray = propPath.EndsWith ("]");
            
            if (isArray)
            {                
                var index = propPath.LastIndexOf ('[');
                clipName = $"{propPath.Substring (index + 1, propPath.Length - index - 2)}: {clipName}";
            }
            

            var backgroundRect = position;
            backgroundRect.height = 4 * (EditorGUIUtility.singleLineHeight) + 1;          
            GUI.Box (backgroundRect, GUIContent.none);
            
            position.height = EditorGUIUtility.singleLineHeight;         
            GUI.Box (position, GUIContent.none);

            EditorGUI.LabelField (position, string.IsNullOrEmpty (clipName) ? label.text : clipName);
            position.y += EditorGUIUtility.singleLineHeight;
            

            EditorGUI.PropertyField (position, clipProp);
            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField (position, volumeProp);
            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField (position, pitchProp);
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return 4 * EditorGUIUtility.singleLineHeight + 1;
        }
    }
}
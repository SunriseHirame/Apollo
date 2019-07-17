using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hirame.Apollo.Editor
{
    [CustomPropertyDrawer (typeof (AudioEventClip))]
    public class AudioEventClipDrawer : PropertyDrawer
    {
        private int cachedClipInstanceId;
        private string cachedClipName;
        
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            // Get properties
            var clipProp = property.FindPropertyRelative ("clip");
            var volumeProp = property.FindPropertyRelative ("volumeMinMax");
            var pitchProp = property.FindPropertyRelative ("pitchMinMax");

            // Find the name of the Attached AudioClip
            var clipName = GetHeaderName (clipProp, label);

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

            EditorGUI.LabelField (position, clipName);
            position.y += EditorGUIUtility.singleLineHeight;
            

            EditorGUI.PropertyField (position, clipProp);
            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField (position, volumeProp);
            position.y += EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField (position, pitchProp);
        }

        private string GetHeaderName (SerializedProperty prop, GUIContent label)
        {
            if (prop.objectReferenceInstanceIDValue != cachedClipInstanceId)
            {
                cachedClipInstanceId = prop.objectReferenceInstanceIDValue;
                cachedClipName = $"{label.text}: {prop.objectReferenceValue.name}";
            }
            
            return prop.objectReferenceValue ? cachedClipName : label.text;
        }
        
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            return 4 * EditorGUIUtility.singleLineHeight + 1;
        }
    }
}
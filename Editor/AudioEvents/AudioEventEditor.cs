using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Hirame.Apollo.Editor
{
    [CustomEditor (typeof (AudioEvent), true)]
    public class AudioEventEditor : UnityEditor.Editor
    {
        private static AudioSource PreviewSource;
        private static string[] typeNames;
        private static Type[] types;
        
        private AudioEvent audioEvent;
        private int typeIndex;

        static AudioEventEditor ()
        {
            types = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                from assemblyType in domainAssembly.GetTypes()
                where typeof(AudioEvent).IsAssignableFrom(assemblyType) && !assemblyType.IsAbstract
                select assemblyType).ToArray();
            
            typeNames = new string[types.Length];
            for (var i = 0; i < types.Length; i++)
            {
                typeNames[i] = types[i].Name;
            }
            
            Array.Sort (typeNames);
        }
        
        private void OnEnable ()
        {          
            audioEvent = target as AudioEvent;
            MatchType ();
        }

        private void MatchType ()
        {
            var typeName = audioEvent.GetType ().Name;
            
            for (var i = 0; i < typeNames.Length; i++)
            {
                if (!typeNames[i].Equals (typeName))
                    continue;
                typeIndex = i;
            }
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update ();

            DrawPreview ();

            if (ChangeType ())
                return;
            
            DrawPropertiesExcluding (serializedObject, "m_Script");
            
            if (serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties ();
            }
        }

        private void DrawPreview ()
        {
            if (GUILayout.Button ("Preview"))
            {
                PlayPreview (audioEvent);
            }
        }

        private bool ChangeType ()
        {
            var newTypeIndex = EditorGUILayout.Popup (typeIndex, typeNames);

            if (newTypeIndex == typeIndex)
                return false;

            typeIndex = newTypeIndex;
            
            var multiEvent = CreateInstance (types[typeIndex]);
            var newId = new SerializedObject (multiEvent).FindProperty ("m_Script").objectReferenceInstanceIDValue;
            
            serializedObject.FindProperty ("m_Script").objectReferenceInstanceIDValue = newId;
            serializedObject.ApplyModifiedProperties ();

            AssetDatabase.Refresh ();
            return true;
        }

        private static void PlayPreview (AudioEvent audioEvent)
        {
            if (PreviewSource == null)
            {
                PreviewSource = new GameObject().AddComponent<AudioSource> ();
                PreviewSource.gameObject.hideFlags = HideFlags.HideAndDontSave;
            }
            
            PreviewSource.Stop ();

            var eventClip = audioEvent.GetEventClip ();
            
            PreviewSource.clip = eventClip.Clip;
            PreviewSource.volume = eventClip.Volume.GetRandom ();
            PreviewSource.pitch = eventClip.Pitch.GetRandom ();
            
            PreviewSource.Play ();
        }
    }
}

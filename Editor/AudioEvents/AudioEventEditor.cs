using UnityEditor;
using UnityEngine;

namespace Hirame.Apollo.Editor
{
    [CustomEditor (typeof (AudioEvent), true)]
    public class AudioEventEditor : UnityEditor.Editor
    {
        private static AudioSource PreviewSource;

        private AudioEvent audioEvent;
        
        private void OnEnable ()
        {          
            audioEvent = target as AudioEvent;
        }

        public override void OnInspectorGUI ()
        {    
            serializedObject.Update ();

            DrawPreview ();
            
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

using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Apollo
{
    public abstract class AudioEvent : ScriptableObject
    {
        internal readonly List<PlayRequest> playQueue = new List<PlayRequest> ();
        
        [SerializeField] private AudioSource audioSourceProto;

        public AudioSource AudioSourceProto => audioSourceProto;

        public void PlayAt (Vector3 position)
        {
            playQueue.Add (new PlayRequest (position));
        }

        public void PlayAt (Transform transform)
        {
            Debug.Log ("PLAY AT");
            playQueue.Add (new PlayRequest (transform.position));
        }

        public void PlayAttachedTo (Transform transform)
        {
            playQueue.Add (new PlayRequest (transform.position, transform));
        }

        public void OverrideWithEventClip (AudioSource audioSource)
        {
            var eventClip = GetEventClip ();
            audioSource.clip = eventClip.Clip;
            audioSource.volume = eventClip.Volume;
            audioSource.pitch = eventClip.Pitch;
        }
        
        internal abstract AudioEventData GetEventClip ();

        

        private void OnEnable ()
        {
            AudioEventPlayer.AddAudioEvent (this);
        }

        private void OnDisable ()
        {
            AudioEventPlayer.RemoveAudioEvent (this);
        }

    }

}
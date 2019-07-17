using System.Runtime.CompilerServices;
using Hirame.Pantheon;
using Unity.Mathematics;
using UnityEngine;

namespace Hirame.Apollo
{
    public abstract class AudioEvent : ScriptableObject
    {
        private const int MaxEventsPerFrame = 4;
        
        [SerializeField] private AudioSource audioSourceProto;
        
        internal int QueuedItems;

        private readonly PlayRequest[] playQueue = new PlayRequest[MaxEventsPerFrame];
        private GameObjectPool<AudioSource> audioSourcePool;

        internal float lastTimePlayed;
        
        public AudioSource AudioSourceProto => audioSourceProto;

        
        public void PlayAt (Vector3 position)
        {
            if (QueuedItems >= MaxEventsPerFrame)
                return;
            
            playQueue[QueuedItems] = new PlayRequest (position);
            QueuedItems++;
        }

        public void PlayAt (Transform transform)
        {
            if (QueuedItems >= MaxEventsPerFrame)
                return;

            playQueue[QueuedItems] = new PlayRequest (transform.position);
            QueuedItems++;
        }

        public void PlayAttachedTo (Transform transform)
        {
            if (QueuedItems >= MaxEventsPerFrame)
                return;
            
            playQueue[QueuedItems] = new PlayRequest (transform.position, transform);
            QueuedItems++;
        }
        

        public void ResolvePlayRequest (int index, float time)
        {
            var playRequest = playQueue[index];
            
            if (!audioSourcePool.TryGetItem (out var eventSource))
            {
                eventSource = Instantiate (
                    audioSourcePool.Proto, playRequest.Position,
                    Quaternion.identity, playRequest.AttachTo);
            }

            ApplyEventClip (eventSource, time);
            eventSource.gameObject.SetActive (true);
            eventSource.Play ();

            new Ananke.DelayedAction (eventSource.clip.length, () => audioSourcePool.AddItem (eventSource));
        }
        
        public void ResolvePlayRequest (int index, float time, out AudioSource eventSource)
        {
            var playRequest = playQueue[index];
            
            if (!audioSourcePool.TryGetItem (out eventSource))
            {
                eventSource = Instantiate (
                    audioSourcePool.Proto, playRequest.Position,
                    Quaternion.identity, playRequest.AttachTo);
            }

            ApplyEventClip (eventSource, time - lastTimePlayed);
            eventSource.Play ();
        }

        public abstract void ApplyEventClip (AudioSource audioSource, float timeSinceLastEvent);

        public abstract ref readonly AudioEventClip GetEventClip ();

        private void OnEnable ()
        {
            audioSourcePool = new GameObjectPool<AudioSource> (audioSourceProto, 10, true);
            audioSourcePool.FillWithItems ();
            
            AudioEventPlayer.AddAudioEvent (this);
        }

        private void OnDisable ()
        {
            AudioEventPlayer.RemoveAudioEvent (this);
        }

    }

}
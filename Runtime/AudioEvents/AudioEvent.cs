using Hirame.Pantheon;
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
        
        internal ActiveAudioEvent ResolvePlayRequest (int index, float time)
        {
            var playRequest = playQueue[index];
            var eventSource = GetAudioSource (in playRequest);

            ApplyEventClip (eventSource, time - lastTimePlayed);
            
            var sourceGo = eventSource.gameObject;
            sourceGo.SetActive (true);

            eventSource.Play ();

            return new ActiveAudioEvent
            {
                OriginPool = audioSourcePool,
                TrackedObject = eventSource,
                TimeToReturn = eventSource.clip.length + time
            };
        }

        private AudioSource GetAudioSource (in PlayRequest playRequest)
        {
            if (!audioSourcePool.TryGetItem (out var eventSource))
            {
                eventSource = Instantiate (
                    audioSourcePool.Proto, playRequest.Position,
                    Quaternion.identity, playRequest.AttachTo);
                
                DontDestroyOnLoad (eventSource.gameObject);
            }

            return eventSource;
        }

        public abstract void ApplyEventClip (AudioSource audioSource, float timeSinceLastEvent);

        public abstract ref readonly AudioEventClip GetEventClip ();

        private void OnEnable ()
        {
            audioSourcePool = new GameObjectPool<AudioSource> (audioSourceProto, 10, true);
            
            if (Application.isPlaying)
                audioSourcePool.FillWithItems ();
            
            AudioEventPlayer.AddAudioEvent (this);
        }

        private void OnDisable ()
        {
            AudioEventPlayer.RemoveAudioEvent (this);
        }

    }

}
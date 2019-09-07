using Hirame.Pantheon;
using UnityEngine;

namespace Hirame.Apollo
{
    public abstract class AudioEvent : ScriptableObject
    {
        private const int MaxEventsPerFrame = 4;

        [Header ("Object Pooling")]
        [SerializeField] protected int startCapacity = 10;
        [SerializeField] protected bool allowExpansion = true;
        [SerializeField] protected AudioSource audioSourceProto;
        
        internal int QueuedItems;

        private readonly PlayRequest[] playQueue = new PlayRequest[MaxEventsPerFrame];
        protected GameObjectPool<AudioSource> audioSourcePool;

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

            if (eventSource == false)
            {
                Debug.Log ("AudioEvent: Failed to resolve play request!");
                return new ActiveAudioEvent ();
            }
            
            var activeEvent = CreateActiveEvent (eventSource, time);
            eventSource.Play ();

            return activeEvent;
        }

        private ActiveAudioEvent CreateActiveEvent (AudioSource eventSource, float time)
        {
            ApplyEventClip (eventSource, time - lastTimePlayed);
            
            var sourceGo = eventSource.gameObject;
            sourceGo.SetActive (true);
            
            return new ActiveAudioEvent
            {
                SourceEvent = this,
                AudioSouce = eventSource,
                EarliestTimeFinished = eventSource.clip.length + time
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

        protected virtual void OnEnable ()
        {
            QueuedItems = 0;
            audioSourcePool = new GameObjectPool<AudioSource> (audioSourceProto, startCapacity, false, allowExpansion);

            if (Application.isPlaying)
            {
                //audioSourcePool.FillWithItems ();
            }
            
            AudioEventSystem.AddAudioEvent (this);
        }

        protected virtual  void OnDisable ()
        {
            AudioEventSystem.RemoveAudioEvent (this);
        }

        internal void ReturnToPool (AudioSource source)
        {
            audioSourcePool.AddItem (source);
        }

    }

}
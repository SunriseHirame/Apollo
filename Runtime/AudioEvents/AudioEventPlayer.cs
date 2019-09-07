using System.Collections.Generic;
using Hirame.Pantheon;
using Hirame.Pantheon.Core;
using UnityEngine;

namespace Hirame.Apollo
{
    public sealed class AudioEventPlayer : GameSystem<AudioEventPlayer>
    {
        private const int MaxActiveEvents = 64;
        
        private static readonly List<AudioEvent> audioEvents = new List<AudioEvent> (32);
        private static readonly ActiveAudioEvent[] activeAudioEvents = new ActiveAudioEvent[MaxActiveEvents];
        private static int activeEventCount;
        
        private void LateUpdate ()
        {
            var time = Time.time;
            
            if (activeEventCount == MaxActiveEvents)
                return;

            for (var i = activeEventCount - 1; i >= 0; i--)
            {
                ref var activeEvent = ref activeAudioEvents[i];
                
                if (time < activeEvent.TimeToReturn)
                    continue;
                
                activeEvent.ReturnToPool ();
                activeEventCount--;
                
                if (activeEventCount > 0)
                    activeAudioEvents[i] = activeAudioEvents[activeEventCount];
            }

            foreach (var audioEvent in audioEvents)
            {
                if (audioEvent.QueuedItems > 0)
                    PlayerQueuedEvents (audioEvent, time);
            }
        }

        private static void PlayerQueuedEvents (AudioEvent audioEvent, float time)
        {
            for (var i = 0; i < audioEvent.QueuedItems; i++)
            {
                var activeEvent = audioEvent.ResolvePlayRequest (i, time);
                if (activeEvent.TrackedObject == false)
                    continue;
                
                activeAudioEvents[activeEventCount++] = activeEvent;
            }

            audioEvent.lastTimePlayed = time;
            audioEvent.QueuedItems = 0;
        }

        internal static void AddAudioEvent (AudioEvent audioEvent)
        {
            audioEvent.lastTimePlayed = Time.time;
            audioEvents.Add (audioEvent);   
        }

        internal static void RemoveAudioEvent (AudioEvent audioEvent)
        {
            audioEvents.Remove (audioEvent);
        }
        
    }
    
    internal struct ActiveAudioEvent
    {
        public GameObjectPool<AudioSource> OriginPool;
        public AudioSource TrackedObject;

        public float TimeToReturn;

        public void ReturnToPool ()
        {
            OriginPool.AddItem (TrackedObject);
            OriginPool = null;
            TrackedObject = null;
        }
    }

}
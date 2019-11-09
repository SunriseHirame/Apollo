using System;
using System.Collections.Generic;
using Hirame.Pantheon;
using Hirame.Pantheon.Core;
using UnityEngine;

namespace Hirame.Apollo
{
    [AutoInitialized]
    public sealed class AudioEventPlayer : GameSystem<AudioEventPlayer>, IWrapUpUpdate
    {
        private const int MaxActiveEvents = 64;
        
        private readonly List<AudioEvent> audioEvents = new List<AudioEvent> (32);
        private readonly ActiveAudioEvent[] activeAudioEvents = new ActiveAudioEvent[MaxActiveEvents];
        private readonly FastStack<int> freeIndexes = new FastStack<int> (MaxActiveEvents);
        
        private int lastUpdateIndex;
        
        void IWrapUpUpdate.OnWarpUpUpdate ()
        {
            var time = Time.time;
            
            if (lastUpdateIndex == MaxActiveEvents)
                return;

            for (var i = 0; i < MaxActiveEvents; i++)
            {
                ref var activeEvent = ref activeAudioEvents[i];
                
                if (!activeEvent.IsAlive || !activeEvent.IsDone (time))
                    continue;
                
                freeIndexes.Push (i);
                activeEvent.IsAlive = false;
                activeEvent.ReturnToPool ();
            }

            foreach (var audioEvent in audioEvents)
            {
                if (audioEvent.QueuedItems > 0)
                    PlayQueuedEvents (audioEvent, time);
            }
        }

        private void PlayQueuedEvents (AudioEvent audioEvent, float time)
        {
            for (var i = 0; i < audioEvent.QueuedItems; i++)
            {
                var activeEvent = audioEvent.ResolvePlayRequest (i, time);
                if (activeEvent.AudioSource == false)
                    continue;

                var index = freeIndexes.Count > 0 ? freeIndexes.Pop () : lastUpdateIndex++;
                if (index >= MaxActiveEvents)
                    throw new IndexOutOfRangeException ("Too many active audio events");

                activeEvent.IsAlive = true;
                activeAudioEvents[index] = activeEvent;
            }

            audioEvent.lastTimePlayed = time;
            audioEvent.QueuedItems = 0;
        }

        internal static void AddAudioEvent (AudioEvent audioEvent)
        {
            audioEvent.lastTimePlayed = Time.time;
            GetOrCreate ().audioEvents.Add (audioEvent);   
        }

        internal static void RemoveAudioEvent (AudioEvent audioEvent)
        {
            GetOrCreate ().audioEvents.Remove (audioEvent);
        }
    }
    
    internal struct ActiveAudioEvent
    {
        public AudioEvent SourceEvent;
        public AudioSource AudioSource;

        public float EarliestTimeFinished;
        public bool IsAlive;

        public bool IsDone (float time)
        {
            return EarliestTimeFinished < time && !AudioSource.isPlaying;
        }

        public void ReturnToPool ()
        {
            SourceEvent.ReturnToPool (AudioSource);
            SourceEvent = null;
            AudioSource = null;
        }
    }

}
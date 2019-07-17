using System.Collections.Generic;
using Hirame.Pantheon;
using Hirame.Pantheon.Core;
using UnityEngine;

namespace Hirame.Apollo
{
    [AutoGameSystem (typeof (AudioEventPlayer))]
    public sealed class AudioEventPlayer : GameSystem<AudioEventPlayer>
    {
        private static readonly List<AudioEvent> audioEvents = new List<AudioEvent>();

        private void LateUpdate ()
        {
            var time = Time.time;

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
                audioEvent.ResolvePlayRequest (i, time);
            }

            audioEvent.lastTimePlayed = time;
            audioEvent.QueuedItems = 0;
        }

        internal static void AddAudioEvent (AudioEvent audioEvent)
        {
            audioEvents.Add (audioEvent);   
        }

        internal static void RemoveAudioEvent (AudioEvent audioEvent)
        {
            audioEvents.Remove (audioEvent);
        }
    }

}
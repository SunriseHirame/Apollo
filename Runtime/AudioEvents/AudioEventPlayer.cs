using System.Collections.Generic;
using Hirame.Pantheon.Core;
using UnityEngine;

namespace Hirame.Apollo
{
    public sealed class AudioEventPlayer : GameSystem<AudioEventPlayer>
    {
        private static readonly List<AudioEvent> audioEvents = new List<AudioEvent>();

        private void Update ()
        {
            foreach (var audioEvent in audioEvents)
            {
                PlayerQueuedEvents (audioEvent);
            }
        }

        private static void PlayerQueuedEvents (AudioEvent audioEvent)
        {
            foreach (var request in audioEvent.playQueue)
            {
                var source = Instantiate (
                    audioEvent.AudioSourceProto, request.Position,
                    Quaternion.identity, request.AttachTo);
                
                audioEvent.OverrideWithEventClip (source);
                
                print ("Resolved request");
            }
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
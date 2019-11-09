using UnityEngine;
using Hirame.Pantheon;
using Unity.Mathematics;

namespace Hirame.Apollo
{
    [CreateAssetMenu (menuName = "Hirame/Audio/Simple Audio Event")]
    public sealed class SimpleAudioEvent : AudioEvent
    {
        public AudioEventClip EventClip = AudioEventClip.Default;

        public override void ApplyEventClip (AudioSource audioSource, float timeSinceLastEvent)
        {
            var attack = math.clamp (timeSinceLastEvent, 1f / QueuedItems, 1f);
            
            audioSource.clip = EventClip.Clip;
            audioSource.volume = EventClip.Volume.GetRandom () * attack;
            audioSource.pitch = EventClip.Pitch.GetRandom ();
        }
        
        public override ref readonly AudioEventClip GetEventClip ()
        {
            return ref EventClip;
        }

    }

}
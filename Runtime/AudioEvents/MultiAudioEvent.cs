using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hirame.Apollo
{
    
    [CreateAssetMenu (menuName = "Hirame/Apollo/Multi Audio Event")]
    public class MultiAudioEvent : AudioEvent
    {
        public AudioEventClip[] EventClips;
        
        public override void ApplyEventClip (AudioSource audioSource, float timeSinceLastEvent)
        {
            var attack = math.clamp (timeSinceLastEvent, 0.1f, 1);

            var eventClip = EventClips[Random.Range (0, EventClips.Length)];
            
            audioSource.clip = eventClip.Clip;
            audioSource.volume = eventClip.Volume.GetRandom () * attack;
            audioSource.pitch = eventClip.Pitch.GetRandom ();
        }

        public override ref readonly AudioEventClip GetEventClip ()
        {
            return ref EventClips[Random.Range (0, EventClips.Length)];
        }

        private void Reset ()
        {
            EventClips = new[] { AudioEventClip.Default };
        }
    }

}
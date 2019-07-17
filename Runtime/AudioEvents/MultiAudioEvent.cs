using UnityEngine;

namespace Hirame.Apollo
{
    
    [CreateAssetMenu (menuName = "Hirame/Apollo/Multi Audio Event")]
    public class MultiAudioEvent : AudioEvent
    {
        public AudioEventClip[] EventClips;
        
        internal override ref readonly AudioEventClip GetEventClip ()
        {
            return ref EventClips[Random.Range (0, EventClips.Length)];
        }

        private void Reset ()
        {
            EventClips = new[] {AudioEventClip.Default};
        }
    }

}
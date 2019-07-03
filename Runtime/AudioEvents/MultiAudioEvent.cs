using UnityEngine;

namespace Hirame.Apollo
{
    
    [CreateAssetMenu (menuName = "Hirame/Apollo/Multi Audio Event")]
    public class MultiAudioEvent : AudioEvent
    {
        public AudioEventClip[] EventClips;
        
        internal override AudioEventData GetEventClip ()
        {
            var eventClip = EventClips[Random.Range (0, EventClips.Length)];
            return new AudioEventData (eventClip.Clip, eventClip.VolumeMinMax.GetRandom (), eventClip.PitchMinMax.GetRandom ());
        }

        private void Reset ()
        {
            EventClips = new[] {AudioEventClip.Default};
        }
    }

}
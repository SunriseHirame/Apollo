using UnityEngine;

namespace Hiramesaurus.Apollo
{
    
    [CreateAssetMenu (menuName = "Hiramesaurus/Apollo/Multi Audio Event")]
    public class MultiAudioEvent : AudioEvent
    {
        public AudioEventClip[] EventClips;
        
        internal override AudioEventData GetEventClip ()
        {
            var eventClip = EventClips[Random.Range (0, EventClips.Length)];
            return new AudioEventData (eventClip.Clip, eventClip.VolumeRange.GetRandom (), eventClip.PitchRange.GetRandom ());
        }

        private void Reset ()
        {
            EventClips = new[] {AudioEventClip.Default};
        }
    }

}
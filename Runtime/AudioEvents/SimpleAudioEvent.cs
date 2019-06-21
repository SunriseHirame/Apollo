using UnityEngine;
using Hiramesaurus.Pantheon;

namespace Hiramesaurus.Apollo
{
    [CreateAssetMenu (menuName = "Hiramesaurus/Apollo/Simple Audio Event")]
    public sealed class SimpleAudioEvent : AudioEvent
    {
        public AudioEventClip EventClip = AudioEventClip.Default;
       
        internal override AudioEventData GetEventClip ()
        {
            return new AudioEventData (EventClip.Clip, EventClip.VolumeRange.GetRandom (), EventClip.PitchRange.GetRandom ());
        }
    }

}
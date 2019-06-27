using UnityEngine;
using Hirame.Pantheon;

namespace Hirame.Apollo
{
    [CreateAssetMenu (menuName = "Hiramesaurus/Apollo/Simple Audio Event")]
    public sealed class SimpleAudioEvent : AudioEvent
    {
        public AudioEventClip EventClip = AudioEventClip.Default;
       
        internal override AudioEventData GetEventClip ()
        {
            return new AudioEventData (EventClip.Clip, EventClip.VolumeMinMax.GetRandom (), EventClip.PitchMinMax.GetRandom ());
        }
    }

}
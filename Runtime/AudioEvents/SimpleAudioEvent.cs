using UnityEngine;
using Hirame.Pantheon;

namespace Hirame.Apollo
{
    [CreateAssetMenu (menuName = "Hirame/Apollo/Simple Audio Event")]
    public sealed class SimpleAudioEvent : AudioEvent
    {
        public AudioEventClip EventClip = AudioEventClip.Default;
       
        internal override ref readonly AudioEventClip GetEventClip ()
        {
            return ref EventClip;
        }
    }

}
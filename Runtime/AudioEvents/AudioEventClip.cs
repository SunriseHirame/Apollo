using Hiramesaurus.Pantheon;
using UnityEngine;

namespace Hiramesaurus.Apollo
{
    [System.Serializable]
    public struct AudioEventClip
    {
        [SerializeField] private AudioClip clip;
        [SerializeField, MinMax (0, 1)] private FloatRange volumeRange;
        [SerializeField, MinMax (-3, 4)] private FloatRange pitchRange;

        public AudioClip Clip => clip;
        public FloatRange VolumeRange => volumeRange;
        public FloatRange PitchRange => pitchRange;
        
        public static AudioEventClip Default => new AudioEventClip
        {
            clip = null,
            volumeRange = new FloatRange (0.7f, 0.7f),
            pitchRange = new FloatRange (1.0f, 1.0f)
        };

        public AudioEventClip (AudioClip clip, FloatRange volumeRange, FloatRange pitchRange)
        {
            this.clip = clip;
            this.volumeRange = FloatRange.Clamped (volumeRange, 0, 1);
            this.pitchRange = FloatRange.Clamped (pitchRange, -3, 4);
        }
    }
}
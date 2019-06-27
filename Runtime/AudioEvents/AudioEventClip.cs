using Hirame.Pantheon;
using UnityEngine;

namespace Hirame.Apollo
{
    [System.Serializable]
    public struct AudioEventClip
    {
        [SerializeField] private AudioClip clip;
        [SerializeField, MinMax (0, 1)] private FloatMinMax volumeMinMax;
        [SerializeField, MinMax (-3, 4)] private FloatMinMax pitchMinMax;

        public AudioClip Clip => clip;
        public FloatMinMax VolumeMinMax => volumeMinMax;
        public FloatMinMax PitchMinMax => pitchMinMax;
        
        public static AudioEventClip Default => new AudioEventClip
        {
            clip = null,
            volumeMinMax = new FloatMinMax (0.7f, 0.7f),
            pitchMinMax = new FloatMinMax (1.0f, 1.0f)
        };

        public AudioEventClip (AudioClip clip, FloatMinMax volumeMinMax, FloatMinMax pitchMinMax)
        {
            this.clip = clip;
            this.volumeMinMax = FloatMinMax.Clamped (volumeMinMax, 0, 1);
            this.pitchMinMax = FloatMinMax.Clamped (pitchMinMax, -3, 4);
        }
    }
}
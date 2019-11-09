using Hirame.Pantheon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hirame.Apollo
{
    [System.Serializable]
    public struct AudioEventClip
    {
        [SerializeField] private AudioClip clip;
        [FormerlySerializedAs ("volumeMinMax")] [SerializeField, MinMax (0, 1)] private FloatMinMax volume;
        [FormerlySerializedAs ("pitchMinMax")] [SerializeField, MinMax (-3, 4)] private FloatMinMax pitch;

        public AudioClip Clip => clip;
        public FloatMinMax Volume => volume;
        public FloatMinMax Pitch => pitch;
        
        public static AudioEventClip Default => new AudioEventClip
        {
            clip = null,
            volume = new FloatMinMax (0.7f, 0.7f),
            pitch = new FloatMinMax (1.0f, 1.0f)
        };

        public AudioEventClip (AudioClip clip, FloatMinMax volumeMinMax, FloatMinMax pitchMinMax)
        {
            this.clip = clip;
            volume = FloatMinMax.Clamped (volumeMinMax, 0, 1);
            pitch = FloatMinMax.Clamped (pitchMinMax, -3, 4);
        }
    }
}
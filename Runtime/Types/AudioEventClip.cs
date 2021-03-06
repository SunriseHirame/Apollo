﻿using Hirame.Pantheon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hirame.Apollo
{
    [System.Serializable]
    public struct AudioEventClip
    {
        [SerializeField] private AudioClip clip;
        [SerializeField, MinMax (0, 1)] private FloatMinMax volume;
        [SerializeField, MinMax (-3, 4)] private FloatMinMax pitch;

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
            volume = volumeMinMax.Clamped (0, 1);
            pitch = pitchMinMax.Clamped (-3, 4);
        }
    }
}
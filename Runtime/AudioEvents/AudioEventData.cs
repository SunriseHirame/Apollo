using UnityEngine;

namespace Hirame.Apollo
{
    public ref struct AudioEventData
    {
        public readonly AudioClip Clip;
        //public readonly Transform AttachedTo;
            
        public readonly float Volume;
        public readonly float Pitch;

        public AudioEventData (AudioClip clip, float volume, float pitch)
        {
            Clip = clip;
            Volume = volume;
            Pitch = pitch;
        }
    }
}
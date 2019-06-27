using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Apollo
{
    public abstract class AudioEvent : ScriptableObject
    {
        public AudioSource AudioSourceProto;
        
        public void PlayAt (Vector3 position)
        {
            
        }

        public void PlayAt (Transform transform)
        {
            
        }

        internal abstract AudioEventData GetEventClip ();
        
        public struct AudioEventData
        {
            public AudioClip Clip;
            public float Volume;
            public float Pitch;

            public AudioEventData (AudioClip clip, float volume, float pitch)
            {
                Clip = clip;
                Volume = volume;
                Pitch = pitch;
            }
        }
    }

}
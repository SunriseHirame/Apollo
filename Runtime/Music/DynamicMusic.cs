using UnityEngine;
using UnityEngine.Audio;

namespace Hirame.Apollo
{
    public abstract class DynamicMusic : ScriptableObject
    {
        [SerializeField] internal AudioMixerGroup AudioMixer;

        public abstract AudioClip GetIntro ();
        
        public abstract AudioClip GetLoop ();
    }

}
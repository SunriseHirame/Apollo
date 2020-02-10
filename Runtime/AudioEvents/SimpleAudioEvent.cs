using System;
using Hirame.Pantheon;
using UnityEngine;
using Unity.Mathematics;

namespace Hirame.Apollo
{
    [CreateAssetMenu (menuName = "Hirame/Audio/Simple Audio Event")]
    public sealed class SimpleAudioEvent : AudioEvent
    {
        public AudioEventClip EventClip = AudioEventClip.Default;
        
        [SerializeField] private InLineAudioEventArray EventClips;
        
        public override void ApplyEventClip (AudioSource audioSource, float timeSinceLastEvent)
        {
            var attack = math.clamp (timeSinceLastEvent, 1f / QueuedItems, 1f);
            
            audioSource.clip = EventClip.Clip;
            audioSource.volume = EventClip.Volume.GetRandom () * attack;
            audioSource.pitch = EventClip.Pitch.GetRandom ();
        }
        
        public override ref readonly AudioEventClip GetEventClip ()
        {
            return ref EventClip;
        }
    }
    
    [Serializable]
    public struct InLineAudioEventArray
    {
        [SerializeField] private bool multiple;
        [SerializeField] private AudioEventClip singleData;
        [SerializeField] private AudioEventClip[] array;

        public AudioEventClip GeRandom ()
        {
            return multiple ? array.GetRandom () : singleData;
        }
    }
}
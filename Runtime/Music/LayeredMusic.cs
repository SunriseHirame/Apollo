using UnityEngine;

namespace Hirame.Apollo
{
    [CreateAssetMenu (menuName = "Hirame/Apollo/Layered Music")]
    public class LayeredMusic : DynamicMusic
    {
        public AudioClip Intro;
        //public AudioClip Outro;
        public AudioClip Loop;
        
        public override AudioClip GetIntro ()
        {
            return Intro;
        }
        
        public override AudioClip GetLoop ()
        {
            return Loop;
        }
    }

}
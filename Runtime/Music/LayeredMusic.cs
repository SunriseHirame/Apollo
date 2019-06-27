using UnityEngine;

namespace Hirame.Apollo
{
    [CreateAssetMenu (menuName = "Hiramesaurus/Apollo/Layered Music")]
    public class LayeredMusic : DynamicMusic
    {
        public AudioClip Intro;
        public AudioClip Outro;
        public AudioClip Loop;
        
        public override AudioClip GetLoop ()
        {
            return Loop;
        }
    }

}
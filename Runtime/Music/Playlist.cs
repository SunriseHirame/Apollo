using UnityEngine;

namespace Hirame.Apollo
{
    [CreateAssetMenu (menuName = "Hirame/Apollo/Playlist")]
    public class Playlist : DynamicMusic
    {
        [SerializeField]
        private LayeredMusic[] tracks;

        public override AudioClip GetIntro ()
        {
            return tracks[0].GetIntro ();
        }
        
        public override AudioClip GetLoop ()
        {
            return tracks[0].GetLoop ();
        }
    }

}

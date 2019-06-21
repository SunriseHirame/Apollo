using UnityEngine;

namespace Hiramesaurus.Apollo
{
    [CreateAssetMenu (menuName = "Hiramesaurus/Apollo/Playlist")]
    public class Playlist : DynamicMusic
    {
        [SerializeField]
        private LayeredMusic[] tracks;

        public override AudioClip GetLoop ()
        {
            return tracks[0].GetLoop ();
        }
    }

}

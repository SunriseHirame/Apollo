using UnityEngine;

namespace Hiramesaurus.Apollo
{
    public class MusicPlayerBase : MonoBehaviour
    {
        public DynamicMusic CurrentMusic { get; protected set; }

        internal MusicPlayerSettings GlobalSettings;

    }
}
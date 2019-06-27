using UnityEngine;

namespace Hirame.Apollo
{
    public class MusicPlayerBase : MonoBehaviour
    {
        public DynamicMusic CurrentMusic { get; protected set; }

        internal MusicPlayerSettings GlobalSettings;

    }
}
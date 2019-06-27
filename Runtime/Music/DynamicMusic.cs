using UnityEngine;

namespace Hirame.Apollo
{
    public abstract class DynamicMusic : ScriptableObject
    {
        public abstract AudioClip GetLoop ();
    }

}
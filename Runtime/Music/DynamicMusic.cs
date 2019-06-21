using UnityEngine;

namespace Hiramesaurus.Apollo
{
    public abstract class DynamicMusic : ScriptableObject
    {
        public abstract AudioClip GetLoop ();
    }

}
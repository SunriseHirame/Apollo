using UnityEngine;

namespace Hirame.Apollo
{
    internal struct PlayRequest
    {
        internal readonly Vector3 Position;
        internal readonly Transform AttachTo;

        internal PlayRequest (Vector3 position, Transform attachTo = default)
        {
            Position = position;
            AttachTo = attachTo;
        }
    }
}
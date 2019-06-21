using UnityEngine;

namespace Hiramesaurus
{
    public class MusicPlayerSettings : ScriptableObject
    {
        [SerializeField] internal AudioSource MusicSourceProto;
        [SerializeField] internal bool ForceMusicTo2D;
    }
}
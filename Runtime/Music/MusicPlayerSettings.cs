using UnityEngine;

namespace Hirame
{
    public class MusicPlayerSettings : ScriptableObject
    {
        [SerializeField] internal AudioSource MusicSourceProto;
        [SerializeField] internal bool ForceMusicTo2D;
    }
}
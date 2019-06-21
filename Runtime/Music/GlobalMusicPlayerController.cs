using UnityEngine;

namespace Hiramesaurus.Apollo
{
    public class GlobalMusicPlayerController : MonoBehaviour
    {

        public enum MusicStartMode
        {
            Intro,
            Loop,
            Outro
        }

        public bool RestartIfSame;
        public MusicStartMode StartFrom;

        [Header ("On Enable")] 
        public bool SetOnEnable;
        public float CrossFadeTime = 0.3f;
        
        public DynamicMusic Music;

        public void PushDynamicMusic (DynamicMusic music)
        {
            GlobalMusicPlayer.Instance.PushDynamicMusic (music, RestartIfSame);
        }

        private void OnEnable ()
        {
            if (SetOnEnable)
            {
                PushDynamicMusic (Music);
            }
        }
    }
    
}
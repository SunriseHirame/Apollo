using UnityEngine;

namespace Hirame.Apollo
{
    public sealed class GlobalMusicPlayer : MusicPlayerBase
    {      
        public static GlobalMusicPlayer Instance { get; private set; }

        private AudioSource musicSource;
        
        private static void RegisterGlobalPlayer (GlobalMusicPlayer player)
        {
            if (Instance != null)
            {
                Debug.Log ($"[{nameof(GlobalMusicPlayer)}]: Trying to register multiple global players! {player.name}.");
                return;
            }
            
            Instance = player;
        }

        public void PushDynamicMusic (DynamicMusic music, bool restart, float crossFadeTime)
        {
            if (!restart && CurrentMusic != null)
            {
                if (CurrentMusic.Equals (music))
                    return;
            }
            
            PlayDynamicMusic (music);
        }
        
        public void PushDynamicMusic (DynamicMusic music, bool restart = false)
        {
            if (!restart && CurrentMusic != null)
            {
                if (CurrentMusic.Equals (music))
                    return;
            }
            
            PlayDynamicMusic (music);
        }

        private void PlayDynamicMusic (DynamicMusic music, float crossFadeTime = 0)
        {
            // TODO:
            // Implement cross fading
            if (musicSource.isPlaying)
                musicSource.Stop ();

            CurrentMusic = music;
            musicSource.clip = music.GetLoop ();
            musicSource.Play ();
        }

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize ()
        {
            var player = new GameObject("Global Music Player").AddComponent<GlobalMusicPlayer> ();
            player.musicSource = player.gameObject.AddComponent<AudioSource> ();
            player.musicSource.loop = true;
            player.hideFlags = HideFlags.DontSave;
            DontDestroyOnLoad (player.gameObject);
            
            RegisterGlobalPlayer (player);
        }
    }
}
using UnityEngine;

namespace Hirame.Apollo
{
    public sealed class GlobalMusicPlayer : MusicPlayerBase
    {      
        public static GlobalMusicPlayer Instance { get; private set; }

        private AudioSource musicSource;
        private AudioSource musicSource2;
        
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

            var intro = music.GetIntro ();
            if (intro)
            {
                musicSource2.clip = intro;
                musicSource2.loop = false;
                musicSource2.Play ();
            }
            
            var loop = music.GetLoop ();
            if (loop)
            {
                musicSource.clip = loop;
                musicSource.PlayDelayed (intro ? intro.length : 0f);
            }
        }

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize ()
        {
            var player = new GameObject("Global Music Player").AddComponent<GlobalMusicPlayer> ();
            player.hideFlags = HideFlags.DontSave;

            DontDestroyOnLoad (player.gameObject);
            RegisterGlobalPlayer (player);

            
            player.musicSource = CreateMusicSource (player.gameObject);
            player.musicSource2 = CreateMusicSource (player.gameObject);
        }

        private static AudioSource CreateMusicSource (GameObject go)
        {
            var source = go.AddComponent<AudioSource> ();
            source.loop = true;
            return source;
        }
    }
}
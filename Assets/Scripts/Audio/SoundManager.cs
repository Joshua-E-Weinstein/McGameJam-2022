using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        private Dictionary<string, AudioSource> AudioSources;

        [SerializeField]
        private AudioClip background;

        private AudioSource backgroundSource;

        private void Awake()
        {
            if (Instance != null && this != Instance)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            AudioSources = new Dictionary<string, AudioSource>();
            if (background)
                StartBackground();
        }

        public void StartBackground()
        {
            if (backgroundSource == null)
                backgroundSource = gameObject.AddComponent<AudioSource>();

            backgroundSource.volume = 0.5f;
            backgroundSource.clip = background;
            backgroundSource.loop = true;
            backgroundSource.Play();
        }

        public void StopBackground()
        {
            backgroundSource.Stop();
        }

        public void PlayClip(string name, AudioClip audioClip, bool restrict = false, float volume = 1)
        {
            if (!AudioSources.ContainsKey(name))
                AudioSources.Add(name, gameObject.AddComponent<AudioSource>());
            if (AudioSources[name].isPlaying && restrict)
                return;

            AudioSources[name].volume = volume;
            AudioSources[name].PlayOneShot(audioClip);
        }

        public void StopClip(string name)
        {
            if (AudioSources.ContainsKey(name))
                AudioSources[name].Stop();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace McgillTeam3
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private string gameLevel;
        [SerializeField] private Slider volumeSlider = null;
        [SerializeField] private Slider microphoneSlider = null;
        [SerializeField] private Toggle microphoneToggle = null;
        [SerializeField] private AudioClip clickClip = null;
        public static float micSensitivity = 0.3f;
        public static float micOverride = -1f;
        
        public void PlayButton()
        {
            SceneManager.LoadScene(gameLevel);
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
        }
        
        public void SetMicrophoneSensitivity(float sensitivity)
        {
            micSensitivity = 1 - sensitivity;
        }

        public void ToggleMicrophone()
        {
            micOverride = microphoneToggle.isOn ? -1f : 2f;
        }

        public void PlayClick()
        {
            SoundManager.Instance.PlayClip("click", clickClip, false, 0.5f);
        }
    }
}

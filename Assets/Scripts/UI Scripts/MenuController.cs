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
        public static float micSensitivity = 0f;
        
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
            if (microphoneToggle.isOn)
            {
                micSensitivity = sensitivity;
            }
        }

        public void ToggleMicrophone()
        {
            micSensitivity = microphoneToggle.isOn ? Single.PositiveInfinity : microphoneSlider.value;
        }
    }
}

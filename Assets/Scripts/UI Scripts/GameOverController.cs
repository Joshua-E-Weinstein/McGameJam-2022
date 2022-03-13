using System;
using System.Collections;
using System.Collections.Generic;
using McgillTeam3.Player_Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace McgillTeam3
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private string menu;
        [SerializeField] private TMP_Text yourScoreText = null;
        [SerializeField] private TMP_Text highScoreText;

        private void Start()
        {
            highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
            yourScoreText.text = Score.PlayerScore.ToString();
        }

        public void ContinueButton()
        {
            SceneManager.LoadScene(menu);
        }
    }
}

using TMPro;
using UnityEngine;

namespace McgillTeam3.Player_Scripts
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        public static int PlayerScore;
        [SerializeField] private int highScore;
        [SerializeField] private int scoreMultiplier = 1;

        [field: SerializeField] public bool EnableScoreCounting { get; set; } 

        private float _elapsedTime;
        private float _timeSinceAwake;
        
        public int CurrentScore
        { 
            get => PlayerScore;
            set
            {
                PlayerScore = value;
                UpdateScoreText();
            }
        }

        private void Start()
        {
            PlayerScore = 0;
            UpdateScoreText(); // Also called here to set the score to the current score if changed in the inspector.
            highScore = PlayerPrefs.GetInt("HighScore", 0);
            scoreText.text = CurrentScore.ToString();
        }

        private void Update()
        {
            if (!EnableScoreCounting)
            {
                _timeSinceAwake = Time.time;
                return;
            }

            /*
            The elapsed time is equal to the current time minus the time since this script awoke.
            This is because Time.time begins counting at the time the script was first awoken, and we
            do not necessarily want to start counting the score right away. 
            */
            _elapsedTime = Time.time - _timeSinceAwake;
            CurrentScore = Mathf.FloorToInt(_elapsedTime * scoreMultiplier);
        }

        private void UpdateScoreText()
        {
            scoreText.text = CurrentScore.ToString();
        }
        
        public bool UpdateHighScore()
        {
            if (CurrentScore <= highScore)
                return false;
            
            highScore = CurrentScore;
            PlayerPrefs.SetInt("HighScore", highScore);

            return true;
        }

        public int GetHighScore() => highScore;
    }
}

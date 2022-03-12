using TMPro;
using UnityEngine;

namespace McgillTeam3.Player_Scripts
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private int _currentScore;
        [SerializeField] private int _highScore;
        [SerializeField] private float _elapsedTime;
        [SerializeField] private int _scoreMultiplier = 1;

        [field: SerializeField] public bool EnableScoreCounting { get; set; } 

        private float _timeSinceAwake;

        public int CurrentScore
        { 
            get => _currentScore;
            set
            {
                _currentScore = value;
                UpdateScoreText();
            }
        }

        private void Start()
        {
            UpdateScoreText(); // Also called here to set the score to the current score if changed in the inspector.
            _highScore = PlayerPrefs.GetInt("HighScore", 0);
            _scoreText.text = CurrentScore.ToString();
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
            CurrentScore = Mathf.FloorToInt(_elapsedTime * _scoreMultiplier);
        }

        private void UpdateScoreText()
        {
            _scoreText.text = CurrentScore.ToString();
        }
        
        public bool UpdateHighScore()
        {
            if (CurrentScore <= _highScore)
                return false;
            
            _highScore = CurrentScore;
            PlayerPrefs.SetInt("HighScore", _highScore);

            return true;
        }
    }
}

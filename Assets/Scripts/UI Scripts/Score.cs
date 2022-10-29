using TMPro;
using UnityEngine;

namespace McgillTeam3.Player_Scripts
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private int _currentScore;
        [SerializeField] private int _highScore;
        [SerializeField] private int _scoreMultiplier = 1;

        private float startTime;

        private bool _enableScoreCounting;
        [SerializeField] public bool EnableScoreCounting { 
            get {
                return _enableScoreCounting;
            } 
            set { 
                _enableScoreCounting = value;
                startTime = Time.time;
                _scoreText.enabled = value;
            }
        } 

        public static int PlayerScore = 0;

        private float _elapsedTime;
        
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
            _elapsedTime = Time.time - startTime;
            CurrentScore = Mathf.FloorToInt(_elapsedTime * _scoreMultiplier);
        }

        private void UpdateScoreText()
        {
            _scoreText.text = CurrentScore.ToString();
        }
        
        public bool UpdateHighScore()
        {
            PlayerScore = CurrentScore;

            if (CurrentScore <= _highScore)
                return false;
            
            _highScore = CurrentScore;
            PlayerPrefs.SetInt("HighScore", _highScore);

            return true;
        }

        public int GetHighScore() => _highScore;
    }
}
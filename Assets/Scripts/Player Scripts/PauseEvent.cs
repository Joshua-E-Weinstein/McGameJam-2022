using System.Collections;
using System.Collections.Generic;
using McgillTeam3.Player_Scripts;
using UnityEngine;

namespace McgillTeam3
{
    public class PauseEvent : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject beginText;
        [SerializeField] private Score score;

        private Echolocation _echolocation;

         private void Awake()
        {
            _echolocation = player.GetComponent<Echolocation>();
        }

        // Start is called before the first frame update
        void Start()
        {
            score.CurrentScore = 0;
            score.EnableScoreCounting = false;
            Time.timeScale = 0;
        }

        private void OnEnable()
        {
            Echolocation.OnStartEcholocate += OnStartEcholocate;
        }

        private void OnDisable()
        {
            Echolocation.OnStartEcholocate -= OnStartEcholocate;
        }

        private void OnStartEcholocate()
        {
            score.EnableScoreCounting = true;
            Time.timeScale = 1;
            gameObject.SetActive(false);
            beginText.SetActive(false);
        }
    }
}

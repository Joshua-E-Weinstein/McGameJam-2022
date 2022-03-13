using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class PauseEvent : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject beginText;

        private Echolocation _echolocation;

         private void Awake()
        {
            _echolocation = player.GetComponent<Echolocation>();
        }

        // Start is called before the first frame update
        void Start()
        {
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
            Time.timeScale = 1;
            gameObject.SetActive(false);
            beginText.SetActive(false);
        }
    }
}

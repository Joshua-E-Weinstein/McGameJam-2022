using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class RedEyesEvent : MonoBehaviour
    {
        private const float MIN_COOLDOWN_TIME = 5;
        private const float MAX_COOLDOWN_TIME = 20;
        private const float MIN_EVENT_TIME = 3;
        private const float MAX_EVENT_TIME = 10;

        private const int MIN_ENEMY_SPAWN = 5;
        private const int MAX_ENEMY_SPAWN = 10;

        [SerializeField]
        // Player controller will be used
        private GameObject player;

        private bool isEventRunning;

        // Start is called before the first frame update
        void Start()
        {
            isEventRunning = false;
            StartCoroutine(Run());
        }

        private void Update()
        {
            bool isEcholocating = Input.GetMouseButtonDown(0);

            // check if event is running and we are echolocating (Check Input or Player class? ).
            if (isEventRunning && isEcholocating)
            {
                Debug.Log("Running");
            }
        }

        IEnumerator Run()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(MIN_COOLDOWN_TIME, MAX_COOLDOWN_TIME));

                StartEvent();

                yield return new WaitForSeconds(Random.Range(MIN_EVENT_TIME, MAX_EVENT_TIME));

                StopEvent();
            }
        }

        private void StartEvent()
        {
            isEventRunning = true;
            Debug.Log("RED EYE EVENT STARTED");
        }

        private void StopEvent()
        {
            isEventRunning = false;
            Debug.Log("RED EYE EVENT FINISHED");
        }
    }
}

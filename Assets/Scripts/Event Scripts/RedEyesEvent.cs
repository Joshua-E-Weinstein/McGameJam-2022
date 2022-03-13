using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class RedEyesEvent : MonoBehaviour
    {
        private const float MIN_COOLDOWN_TIME = 5f;
        private const float MAX_COOLDOWN_TIME = 6f;
        private const float MIN_EVENT_TIME = 3f;
        private const float MAX_EVENT_TIME = 10f;

        private const int MIN_ENEMY_SPAWN = 5;
        private const int MAX_ENEMY_SPAWN = 10;
        private const float MIN_ENEMY_RANGE = 2.5f;
        private const float MAX_ENEMY_RANGE = 5f;
        private const float WARN_TIME = 3f;

        [SerializeField]
        // Player controller will be used
        private GameObject player;

        [SerializeField]
        private GameObject enemy;

        [SerializeField]
        private ParticleSystem warningParticles;

        private bool isEventRunning;
        private bool isTrackingEcho;
        private ShaderController shaderController;

        // Start is called before the first frame update
        void Start()
        {
            isEventRunning = false;
            isTrackingEcho = false;
            shaderController = player.GetComponent<ShaderController>();
            StartCoroutine(Run());
        }

        private void Update()
        {
            // check if event is running and we are echolocating (Check Input or Player class? ).
            if (isEventRunning)
                UpdateParticles();
            if (isTrackingEcho && shaderController._echolocating)
                SpawnEnemies();
        }

        private void UpdateParticles()
        {
            warningParticles.transform.position = player.transform.position;
        }

        IEnumerator Run()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(MIN_COOLDOWN_TIME, MAX_COOLDOWN_TIME));

                StartEvent();

                yield return new WaitForSeconds(WARN_TIME);

                isTrackingEcho = true;

                yield return new WaitForSeconds(Random.Range(MIN_EVENT_TIME, MAX_EVENT_TIME));

                StopEvent();
            }
        }

        private void StartEvent()
        {
            if (isEventRunning)
                return;
            isEventRunning = true;
            warningParticles.Clear();
            warningParticles.Play();
            Debug.Log("RED EYE EVENT STARTED");
        }

        private void StopEvent()
        {
            // If we already spawned enemies, don't stop the event again.
            if (!isEventRunning)
                return;
            isEventRunning = false;
            isTrackingEcho = false;
            warningParticles.Stop();
            Debug.Log("RED EYE EVENT FINISHED");
        }

        private void SpawnEnemies()
        {
            int enemyAmount = Random.Range(MIN_ENEMY_SPAWN, MAX_ENEMY_SPAWN);
            for (int i = 0; i < enemyAmount; i++)
            {
                float distanceY = Random.Range(MIN_ENEMY_RANGE, MAX_ENEMY_RANGE);
                float distanceX = Random.Range(MIN_ENEMY_RANGE, MAX_ENEMY_RANGE);

                float positionX = Random.Range(0f, 1f) < 0.5 ? player.transform.position.x + distanceX : player.transform.position.x - distanceX;
                float positionY = Random.Range(0f, 1f) < 0.5 ? player.transform.position.y + distanceY : player.transform.position.y - distanceY;
                Vector3 position = new Vector3(positionX, positionY);
                Quaternion rotation = Quaternion.LookRotation(player.transform.position, player.transform.position);
                
                Instantiate(enemy, position, rotation);
            }

            StopEvent();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem deathParticles;

        [SerializeField]
        private AudioClip deathClip;

        private const float TRACKING_TIME = 10f;
        private const float SPEED = 5f;

        private GameObject player;
        private bool isTracking;
        private Vector3 distance;
        private Vector3 lastPosition;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player");
            distance = new Vector3()
            {
                x = transform.position.x - player.transform.position.x,
                y = transform.position.y - player.transform.position.y
            };
            StartCoroutine(Track());
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // if tracking player, track
            if (isTracking)
                TrackPosition();
            else
                MoveTo();
        }

        void TrackPosition()
        {
            Vector3 newPosition = new Vector3()
            {
                x = player.transform.position.x + distance.x,
                y = player.transform.position.y + distance.y
            };

            float step = SPEED * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
        }

        void MoveTo()
        {
            float step = SPEED * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, lastPosition, step);
            transform.LookAt(lastPosition);

            if (Vector3.Distance(transform.position, lastPosition) < 0.5f)
                Die();
        }

        void Die()
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            SoundManager.Instance.PlayClip("enemy_death", deathClip, false, 0.1f);

            Object.Destroy(gameObject);
        }

        IEnumerator Track()
        {
            isTracking = true;
            yield return new WaitForSeconds(TRACKING_TIME);
            lastPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            isTracking = false;
        }
    }
}

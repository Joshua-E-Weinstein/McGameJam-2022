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

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private const float TRACKING_TIME = 2f;
        private const int GLOW_COUNT = 2;
        private const float SPEED = 7f;

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

            Vector3 scale = transform.localScale;
            if (transform.position.x < player.transform.position.x && scale.x < 0)
                scale.x = -scale.x;
            if (transform.position.x > player.transform.position.x && scale.x > 0)
                scale.x = -scale.x;
            transform.localScale = scale;
        }

        void MoveTo()
        {
            float step = SPEED * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, lastPosition, step);
            print(lastPosition);

            if (Vector3.Distance(transform.position, lastPosition) < 0.001f)
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

            const float INTERVAL = TRACKING_TIME / GLOW_COUNT / 2f;
            Color oldColor = spriteRenderer.color;

            for (int i = 0; i < GLOW_COUNT; i++)
            {
                spriteRenderer.color = new Color(oldColor.r, oldColor.g + 255f, oldColor.b, oldColor.a);
                yield return new WaitForSeconds(INTERVAL);
                spriteRenderer.color = oldColor;
                yield return new WaitForSeconds(INTERVAL);
                print($"Done {INTERVAL}");
            }

            lastPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            isTracking = false;
        }
    }
}

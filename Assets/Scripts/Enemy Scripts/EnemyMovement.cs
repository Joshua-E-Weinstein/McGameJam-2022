using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class EnemyMovement : MonoBehaviour
    {
        private const float TRACKING_TIME = 10f;
        private const float SPEED = 20f;

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
            Debug.Log(distance);
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

            transform.position = newPosition;
        }

        void MoveTo()
        {
            float step = SPEED * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, lastPosition, step);

            if (Vector3.Distance(transform.position, lastPosition) < 0.5f)
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

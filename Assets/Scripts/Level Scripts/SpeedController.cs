using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class SpeedController : MonoBehaviour
    {
        // Start is called before the first frame update
        public const float MIN_SPEED = 1f;
        public const float MAX_SPEED = 3f;
        public const float ACCELERATION = 1/6000f;
        public static float speed;
        void Start()
        {
            speed = MIN_SPEED;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (speed < MAX_SPEED) speed += ACCELERATION;
        }
    }
}

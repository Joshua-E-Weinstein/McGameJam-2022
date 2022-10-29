using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb2d;
        GameObject player;
        float speed;
        Vector3 distance;
        bool jumped = false;
        [SerializeField] Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            gameObject.transform.position = new Vector3 (gameObject.transform.position[0] - CaveGen.speed * SpeedController.speed, gameObject.transform.position[1], gameObject.transform.position[2]);
            if (!jumped){
                Vector2 distance = (Vector2) player.transform.position - (Vector2) gameObject.transform.position;
                if (distance.magnitude < 5f){
                    rb2d.AddForce(20f * distance.normalized, ForceMode2D.Impulse);
                    jumped = true;
                    animator.SetBool("Slither", true);
                }
            }
        }
    }
}

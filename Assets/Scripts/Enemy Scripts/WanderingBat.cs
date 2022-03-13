using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class WanderingBat : MonoBehaviour
    {
        GameObject player;
        Vector3 distance;
        Vector2 velocity;
        bool diving = false;

        // Start is called before the first frame update
        
        private void OnEnable()
        {
            player = GameObject.FindWithTag("Player");
            
            Echolocation.OnStartEcholocate += OnStartEcholocate;
        }

        private void OnDisable()
        {
            Echolocation.OnStartEcholocate -= OnStartEcholocate;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!diving){
                gameObject.transform.position = new Vector3 (gameObject.transform.position[0] - CaveGen.speed * SpeedController.speed, gameObject.transform.position[1], gameObject.transform.position[2]);
            }
            else gameObject.transform.position += (Vector3) velocity;

            if (gameObject.transform.position.magnitude > 30) GameObject.Destroy(gameObject);
        }

        void OnStartEcholocate(){
            if(!diving){
                Vector3 distance = player.transform.position - gameObject.transform.position;
                if (distance.magnitude < 12){
                    diving = true;
                    velocity = (Vector2) distance.normalized * 0.3f * SpeedController.speed;
                }
            }
        }
    }
}

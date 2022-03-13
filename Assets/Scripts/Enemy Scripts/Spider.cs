using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class Spider : MonoBehaviour
    {
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] LineRenderer thread;
        Vector3 pos;
        float xPos;
        float offset;
        // Start is called before the first frame update
        void Start()
        {
            offset = Random.Range(0f, Mathf.PI);
            float size = Random.Range(0.75f, 1f);
            sprite.flipX = Random.value > 0.5f;
            gameObject.transform.localScale = new Vector3 (size, size, size);
            xPos = gameObject.transform.position.x;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            pos = gameObject.transform.position;
            pos.Set(xPos + 0.5f * Mathf.Sin(3 * (Time.time + offset)), pos[1] - CaveGen.speed * SpeedController.speed, -6);
            gameObject.transform.position = pos;
            
            thread.SetPositions(new Vector3[] {new Vector3(xPos, 25, -5), pos});

            xPos -= CaveGen.speed * SpeedController.speed;

            if (xPos < -10){
                GameObject.Destroy(gameObject);
            }
        }
    }
}

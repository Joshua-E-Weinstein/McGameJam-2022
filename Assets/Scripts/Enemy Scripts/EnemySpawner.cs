using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject spider;
        [SerializeField] GameObject bat;
        float delay = 5f;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            delay -= Time.deltaTime;

            if (delay <= 0){
                int enemyRoll = Random.Range(0, 2);
                switch(enemyRoll){
                    case (0):
                        SpawnSpiders();
                        break;
                    case (1):
                        SpawnBats();
                        break;
                }
                

            }
        }

        void SpawnSpiders(){
            int count = Random.Range(3, 13);
            delay += 2 * (count + Random.Range(0, 3));

            Vector2 spawnPos = new Vector2(12f, 12f);
            while (count > 0){
                GameObject.Instantiate(spider, (Vector3) spawnPos + new Vector3(0, Random.Range(-7f, 7f), -5), new Quaternion());
                float spawnOffset = Random.Range(2f, 4f);
                spawnPos += new Vector2(spawnOffset, spawnOffset);
                count --;
            }
        }

        void SpawnBats(){
            int count = Random.Range(1, 3);
            delay += 4 * (count + Random.Range(0, 3));

            float spawnX = 12f;
            while (count > 0){
                GameObject.Instantiate(bat, new Vector3 (spawnX, Random.Range(-4f, 4f), -5), new Quaternion());
                spawnX += Random.Range(2f, 4f);
                count --;
            }
        }
    }
}

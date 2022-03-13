using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace McgillTeam3
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] Animator playerAnimator;
        [SerializeField] Animator[] heartAnimators;
        [SerializeField] public int HP;
        const float INVULN_DURATION = 2f;
        float invuln = 0.5f;

        void Start(){
            playerAnimator.SetBool("Invulnerable", false);
            invuln = 0.5f;
        }

        void Update(){
            if (invuln > 0f){ 
                invuln -= Time.deltaTime;
                if (invuln <= 0f) playerAnimator.SetBool("Invulnerable", false);
            }
        }

        void OnCollisionStay2D(Collision2D collision){
            {
                if (invuln <= 0f){
                    if (collision.gameObject.tag == "Damaging")
                    HP -= 1;
                    heartAnimators[HP].SetTrigger("LoseHeart");
                    if (HP <= 0) Die();
                    else {
                        invuln = INVULN_DURATION;
                        playerAnimator.SetBool("Invulnerable", true);
                    }
                }
            }
        }

        void Die(){
            SceneManager.LoadScene("GameOver");
        }
    }
}

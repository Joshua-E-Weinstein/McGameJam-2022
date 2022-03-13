using System.Collections;
using System.Collections.Generic;
using McgillTeam3.Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace McgillTeam3
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] Animator playerAnimator;
        [SerializeField] Animator[] heartAnimators;
        [SerializeField] AudioClip damageSound;

        [SerializeField] public int HP;
        [SerializeField] private Score score;
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
                    if (HP >= 0) heartAnimators[HP].SetTrigger("LoseHeart");
                    if (HP <= 0) Die();
                    else {
                        SoundManager.Instance.PlayClip("Hurt", damageSound, false, 0.5f);
                        invuln = INVULN_DURATION;
                        playerAnimator.SetBool("Invulnerable", true);
                    }
                }
            }
        }

        void Die()
        {
            score.UpdateHighScore();
            /*score.EnableScoreCounting = false;
            score.ResetCurrentScore();*/
            SceneManager.LoadScene("GameOver");
        }
    }
}

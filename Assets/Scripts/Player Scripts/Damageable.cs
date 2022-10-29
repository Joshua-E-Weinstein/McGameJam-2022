using System.Collections;
using System.Collections.Generic;
using McgillTeam3.Player_Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace McgillTeam3
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] ParticleSystem deathParticles;
        [SerializeField] Animator playerAnimator;
        [SerializeField] LineRenderer tether;
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] Animator[] heartAnimators;
        [SerializeField] AudioClip damageSound;

        [SerializeField] public int HP;
        [SerializeField] private Score score;
        const float INVULN_DURATION = 2f;
        float invuln = 0.5f;
        bool dead = false;

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
                    if (collision.gameObject.tag == "Damaging") {
                        HP -= 1;
                        SoundManager.Instance.PlayClip("Hurt", damageSound, false, 0.5f);
                    }
                    if (HP >= 0) heartAnimators[HP].SetTrigger("LoseHeart");
                    invuln = INVULN_DURATION;
                    if (HP <= 0) Die();
                    else {
                        playerAnimator.SetBool("Invulnerable", true);
                    }
                }
            }
        }

        void Die()
        {
            if (!dead){
                dead = true;
                score.UpdateHighScore();
                sprite.enabled = false;
                gameObject.GetComponent<MouseMovement>().enabled = false;
                gameObject.GetComponent<Echolocation>().enabled = false;
                tether.enabled = false;
                StartCoroutine(GameOver());
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                /*score.EnableScoreCounting = false;
                score.ResetCurrentScore();*/
            }
        }

        IEnumerator GameOver(){
            yield return new WaitForSeconds (2f);
            SceneManager.LoadScene("GameOver");
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace McgillTeam3
{
    public class GameOver : MonoBehaviour
    {
        // Start is called before the first frame update
        public static IEnumerator EndGame(){
            yield return new WaitForSeconds (3f);
            SceneManager.LoadScene("GameOver");
            yield return null;
        }
    }
}

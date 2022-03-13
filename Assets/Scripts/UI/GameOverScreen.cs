using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace McgillTeam3
{
    public class GameOverScreen : MonoBehaviour
    {
        // Start is called before the first frame update
        void OnEcholocate(){
            SceneManager.LoadScene("MainGame");
        }
    }
}

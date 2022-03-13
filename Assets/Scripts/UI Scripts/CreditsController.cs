using System;
using System.Collections;
using System.Collections.Generic;
using McgillTeam3.Player_Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace McgillTeam3
{
    public class CreditsController : MonoBehaviour
    {
        [SerializeField] private string menu;

        public void ExitToMenu()
        {
            SceneManager.LoadScene(menu);
        }
    }
}

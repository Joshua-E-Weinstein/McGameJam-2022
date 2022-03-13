using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class Heart : MonoBehaviour
    {
        // Start is called before the first frame update
        public void DisableHeart(){
            gameObject.SetActive(false);
        }

    }
}

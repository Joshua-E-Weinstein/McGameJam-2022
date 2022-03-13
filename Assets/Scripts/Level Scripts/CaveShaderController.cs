using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class CaveShaderController : MonoBehaviour
    {
        [SerializeField] CaveGen caveGenerator;
        [SerializeField] Material wallMaterial;
        Vector2 offset = new Vector2();

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            offset.Set(offset[0] + caveGenerator.speed * SpeedController.speed, 0f);
            wallMaterial.SetVector("_Offset", offset);
        }
    }
}

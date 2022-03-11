using UnityEngine;

namespace McgillTeam3
{
    public class Element : MonoBehaviour
    {
        public ElementRuntimeSet RuntimeSet;

        private void OnEnable()
        {
            RuntimeSet.Add(this);
        }

        private void OnDisable()
        {
            RuntimeSet.Remove(this);
        }
    }
}
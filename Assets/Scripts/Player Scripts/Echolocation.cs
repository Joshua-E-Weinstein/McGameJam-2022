using System;
using UnityEngine;

namespace McgillTeam3
{
    public class Echolocation : MonoBehaviour
    {

        #region Events

        public delegate void StartEcholocate();

        public static event StartEcholocate OnStartEcholocate;
        
        public delegate void EndEcholocate();

        public static event EndEcholocate OnEndEcholocate;
        
        #endregion
        
        private PlayerControls _playerControls;

        private void Awake()
        {
            _playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        // Start is called before the first frame update
        void Start()
        {
            _playerControls.Echolocation.Echolocate.started += _ =>
            {
                if (OnStartEcholocate != null) OnStartEcholocate();
            };
            _playerControls.Echolocation.Echolocate.canceled += _ =>
            {
                if (OnEndEcholocate != null) OnEndEcholocate();
            };
        }

        public Vector3 GetPlayerPosition()
        {
            return transform.position;
        }
    }
}

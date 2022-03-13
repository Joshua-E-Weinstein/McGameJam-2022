using System;
using UnityEngine;
using UnityEngine.UI;

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

        float sensitivity = Math.Max(MenuController.micSensitivity, MenuController.micOverride);
        private bool _yelling;
        private bool _wasYelling;

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

        // Update is called once per frame
        void Update()
        {
            float loudness = MicInput.MicLoudness;
            if (loudness > sensitivity)
            {
                _yelling = true;
                if (!_wasYelling){
                    _wasYelling = true;
                    OnStartEcholocate();
                }
            }
            else
            {
                _yelling = false;
                if (_wasYelling){
                    _wasYelling = false;
                    OnEndEcholocate();
                }
            }
        }
    }
}

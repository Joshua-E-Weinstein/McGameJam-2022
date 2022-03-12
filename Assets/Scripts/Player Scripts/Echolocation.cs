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
            float db = MicInput.MicLoudness;

            if (db > 0.3f)
            {
                Debug.Log("ECHO");
                _yelling = true;
                if (!_wasYelling){
                    _wasYelling = true;
                    OnStartEcholocate();
                }
            }
            else
            {
                Debug.Log("");
                _yelling = false;
                if (_wasYelling){
                    _wasYelling = false;
                    OnEndEcholocate();
                }
            }

            //Debug.Log("Volume is " + MicInput.MicLoudness.ToString("##.#####") + ", decibels is :" + MicInput.MicLoudnessinDecibels.ToString("######"));
            //Debug.Log("Volume is " + NormalizedLinearValue(MicInput.MicLoudness).ToString("#.####") + ", decibels is :" + NormalizedDecibelValue(MicInput.MicLoudnessinDecibels).ToString("#.####"));
        }
    }
}

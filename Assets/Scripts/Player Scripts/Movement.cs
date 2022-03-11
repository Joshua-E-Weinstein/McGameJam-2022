using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform cam;
        [SerializeField] private float move_speed;

        private float _h_input;
        private float _v_input;
        private float _camera_angle;
        private float _rotate_angle;
        private Vector3 _direction;
        private float _smooth_time = 0.1f;
        
        void Update()
        {
            _h_input = Input.GetAxisRaw("Horizontal");
            _v_input = Input.GetAxisRaw("Vertical");
            _direction = new Vector3(_h_input, 0f, _v_input).normalized;

            if (_direction.magnitude >= 0.1f) {
                _camera_angle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                _rotate_angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _camera_angle, ref _smooth_time, 0.1f);
                transform.rotation = Quaternion.Euler(0f, _rotate_angle, 0f);

                _direction = Quaternion.Euler(0f, _camera_angle, 0f) * Vector3.forward;
                controller.Move(_direction * move_speed * Time.deltaTime);
            }
        }
    }
}

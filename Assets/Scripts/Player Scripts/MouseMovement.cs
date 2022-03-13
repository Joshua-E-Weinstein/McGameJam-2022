using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace McgillTeam3
{
    public class MouseMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rigidBody;
        [SerializeField] private FloatReference idle_distance;

        [SerializeField] private FloatReference acceleration;
        [SerializeField] private FloatReference deceleration;
        [SerializeField] private FloatReference max_move_speed;

        private float move_speed = 0f;

        private Vector2 _targetPos = Vector2.zero;


        void OnApproach(InputValue value)
        {
            _targetPos = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        }

        void Update()
        {
            Vector2 direction = _targetPos - (Vector2) transform.position; // Direction towards the mouse.
            
            // Update velocity towards mouse.
            rigidBody.velocity = 2f * direction;
        }
    }
}
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

        private float move_speed = 2f;

        private Vector2 _targetPos = Vector2.zero;

        [SerializeField] LineRenderer tether;

        void OnApproach(InputValue value)
        {
            _targetPos = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        }

        void Update()
        {
            Vector2 direction = _targetPos - (Vector2) transform.position; // Direction towards the mouse.

            Vector2 point0 = _targetPos - 0.1f * direction.normalized;
            tether.SetPosition(0, new Vector3(point0[0], point0[1], -5));

            Vector2 point1 = (Vector2) transform.position + 1f * direction.normalized;
            tether.SetPosition(1, new Vector3(point1[0], point1[1], -5));

            if (direction.magnitude <= 1.1f && tether.enabled) tether.enabled = false;
            else if (direction.magnitude > 0.6f && !tether.enabled) tether.enabled = true;
            
            // Update velocity towards mouse.
            rigidBody.velocity = move_speed * direction;
        }
    }
}
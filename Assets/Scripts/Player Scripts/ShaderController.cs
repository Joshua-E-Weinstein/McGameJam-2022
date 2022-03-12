using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace McgillTeam3
{
    public class ShaderController : MonoBehaviour
    {
        [SerializeField] private Material wallMaterial;
        [SerializeField] private GameObject player;
        [SerializeField] private float rippleMaxDistance = 10f;
        [SerializeField] private float rippleDuration = 0.2f;
        [SerializeField] private float fadeDuration = 0.15f;
        [SerializeField] private string rippleCenter= "_Ripple_Center";
        [SerializeField] private string rippleDistance = "_Ripple_Distance";
        [SerializeField] private string fadeAmount = "_Fade_Amount";

        private bool _echolocating;

        private Echolocation _echolocation;

        private void Awake()
        {
            _echolocation = player.GetComponent<Echolocation>();
        }

        private void OnEnable()
        {
            Echolocation.OnStartEcholocate += OnStartEcholocate;
            Echolocation.OnEndEcholocate += OnEndEcholocate;
        }

        private void OnDisable()
        {
            Echolocation.OnStartEcholocate -= OnStartEcholocate;
            Echolocation.OnEndEcholocate -= OnEndEcholocate;
        }

        private void OnStartEcholocate()
        {
            StartCoroutine("Echolocating");
        }

        private void OnEndEcholocate()
        {
            StartCoroutine("Blinding");
        }

        IEnumerator Echolocating()
        {
            _echolocating = true;
            wallMaterial.SetFloat(fadeAmount, 0f);
            float time = 0;
            float rippleStartValue = wallMaterial.GetFloat(rippleDistance);
            while (_echolocating  && time < rippleDuration)
            {
                wallMaterial.SetFloat(rippleDistance, Mathf.Lerp(rippleStartValue, rippleMaxDistance, time / rippleDuration));
                time += Time.deltaTime;
                yield return null;
            }
            wallMaterial.SetFloat(rippleDistance, rippleMaxDistance);
        }
        
        IEnumerator Blinding()
        {
            _echolocating = false;
            float time = 0;
            float fadeStartValue = wallMaterial.GetFloat(fadeAmount);
            while (!_echolocating && time < fadeDuration)
            {
                wallMaterial.SetFloat(fadeAmount, Mathf.Lerp(fadeStartValue, 1, time / fadeDuration));
                time += Time.deltaTime;
                yield return null;
            }
            wallMaterial.SetFloat(fadeAmount, 1f);
            wallMaterial.SetFloat(rippleDistance, 0f);
        }
        
        private void Update()
        {
            wallMaterial.SetVector(rippleCenter, player.transform.position);
        }
    }
}

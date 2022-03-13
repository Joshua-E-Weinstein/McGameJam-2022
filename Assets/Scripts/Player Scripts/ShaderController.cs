using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private AudioClip sonarClip;
        [SerializeField] private AudioClip decompression;

        public bool _echolocating;

        private Echolocation _echolocation;

        const float MAX_BREATH = 2f;
        [Range(0, MAX_BREATH)] float currentBreath = MAX_BREATH;
        bool breathless;
        [SerializeField] RectTransform breathBar;
        [SerializeField] Image breathBarImage;

        float time = 0;
        float rippleStartValue;
        float fadeStartValue;

        private void Awake()
        {
            _echolocation = player.GetComponent<Echolocation>();
        }

        private void Start()
        {
            rippleStartValue = 0f;
            fadeStartValue = 1f;
            wallMaterial.SetFloat(rippleDistance, 0f);
            wallMaterial.SetFloat(fadeAmount, 1f);
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
            if (currentBreath > 0 && !breathless){
                SoundManager.Instance.PlayClip("sonar", sonarClip, true, 0.1f);
                time = 0;
                wallMaterial.SetFloat(fadeAmount, 0);
                rippleStartValue = wallMaterial.GetFloat(rippleDistance);
                _echolocating = true;
            }
        }

        private void OnEndEcholocate()
        {
            time = 0;
            fadeStartValue = wallMaterial.GetFloat(fadeAmount);
           _echolocating = false;
        }
        
        private void Update()
        {
            wallMaterial.SetVector(rippleCenter, player.transform.position);


            // print(_echolocating);
            // print(time);

            if (_echolocating && time < rippleDuration){
                wallMaterial.SetFloat(rippleDistance, Mathf.Lerp(rippleStartValue, rippleMaxDistance, time / rippleDuration));
                time += Time.deltaTime;
            }
            else if (_echolocating && time >= rippleDuration){
                wallMaterial.SetFloat(rippleDistance, rippleMaxDistance);
            }
            else if (!_echolocating && time < fadeDuration)
            {
                wallMaterial.SetFloat(fadeAmount, Mathf.Lerp(fadeStartValue, 1, time / fadeDuration));
                time += Time.deltaTime;
            }
            else if (!_echolocating && time >= fadeDuration)
            {
                wallMaterial.SetFloat(fadeAmount, 1f);
                wallMaterial.SetFloat(rippleDistance, 0);
            }

            if (_echolocating && currentBreath > 0) currentBreath -= Time.deltaTime;
            else if (currentBreath < MAX_BREATH) currentBreath += (Time.deltaTime * 0.25f);
            if (currentBreath < 0.01f) {
                OnEndEcholocate();
                breathless = true;
                breathBarImage.color = new Color(1f, 0.3f, 0.3f, 1f);
                SoundManager.Instance.PlayClip("Air Hiss", decompression, false, 0.5f);
            }
            if (breathless && currentBreath >= 0.5f * MAX_BREATH){
                breathless = false;
                breathBarImage.color = new Color(1f, 1f, 1f, 1f);
            } 
            breathBar.transform.localScale = new Vector3(currentBreath/MAX_BREATH, 1f, 1f);
        }
    }
}

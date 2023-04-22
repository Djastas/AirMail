using DayNightSystem;
using StvDEV.StarterPack;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CorpKaktus.scripts.System.DayNightSystem
{
    public class DayCycleManager : MonoBehaviourSingleton<DayCycleManager>
    {
        [Range(0, 1)]
        public float timeOfDay;
    
        [SerializeField] private float dayDuration = 30f;

        [SerializeField] private AnimationCurve sunCurve;
        [SerializeField] private AnimationCurve moonCurve;
        [SerializeField] private AnimationCurve skyboxCurve;

        [SerializeField] private Material daySkybox;
        [SerializeField] private Material nightSkybox;

        [SerializeField] private ParticleSystem stars;

        [SerializeField] private Light sun;
        [SerializeField] private Light moon;

        private float _sunIntensity;
        private float _moonIntensity;

        private DayNightTrigger[] _dayNightTriggers;

        private bool _dayIsStart;
        private bool _nightIsStart;

        private void Awake()
        {
            _dayNightTriggers = FindObjectsOfType<DayNightTrigger>();
        }

        protected override void Start()
        {
            _sunIntensity = sun.intensity;
            _moonIntensity = moon.intensity;
        }

        private void Update()
        {
            timeOfDay += Time.deltaTime / dayDuration;
            if (timeOfDay >= 1) timeOfDay -= 1;

            // Настройки освещения (skybox и основное солнце)
            RenderSettings.skybox.Lerp(nightSkybox, daySkybox, skyboxCurve.Evaluate(timeOfDay));
            RenderSettings.sun = skyboxCurve.Evaluate(timeOfDay) > 0.1f ? sun : moon;
            DynamicGI.UpdateEnvironment();

            // Прозрачность звёзд
            var mainModule = stars.main;
            //mainModule.startColor = new Color(1, 1, 1, 1 - SkyboxCurve.Evaluate(TimeOfDay));

            // Поворот луны и солнца
            sun.transform.localRotation = Quaternion.Euler(timeOfDay * 360f, 180, 0);
            moon.transform.localRotation = Quaternion.Euler(timeOfDay * 360f + 180f, 180, 0);

            // Интенсивность свечения луны и солнца
            sun.intensity = _sunIntensity * sunCurve.Evaluate(timeOfDay);
            moon.intensity = _moonIntensity * moonCurve.Evaluate(timeOfDay);
            if ( timeOfDay < 0.5  && !_dayIsStart)
            {
                foreach (var dayNightTrigger in _dayNightTriggers)
                {
                    ExecuteEvents.Execute<IDayNight>(dayNightTrigger.gameObject, null, (x,y)=>x.OnStartDay());
                }

                _dayIsStart = true;
                _nightIsStart = false;
            }
         
            if ( timeOfDay > 0.5  && !_nightIsStart)
            {
                foreach (var dayNightTrigger in _dayNightTriggers)
                {
                    ExecuteEvents.Execute<IDayNight>(dayNightTrigger.gameObject, null, (x,y)=>x.OnStartNight());
                }

                _nightIsStart = true;
                _dayIsStart = false;
            }
        
        
        
        
       
        }
    }
}

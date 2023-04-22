using UnityEngine;
using UnityEngine.Events;

namespace DayNightSystem
{
    public class DayNightTrigger : MonoBehaviour , IDayNight
    {
        [SerializeField] private UnityEvent dayStartAction;
        [SerializeField] private UnityEvent nightStartAction;
        
        
        
        public void OnStartDay()
        {
            dayStartAction?.Invoke();
        }

        public void OnStartNight()
        {
            nightStartAction?.Invoke();
        }
    }
}

using UnityEngine.EventSystems;

namespace DayNightSystem
{
    public interface IDayNight : IEventSystemHandler
    {
        // functions that can be called via the messaging system
        void OnStartDay();
        void OnStartNight();
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

namespace Softgames.MainMenu
{
    public class EventSystemDisabler : MonoBehaviour
    {
        [SerializeField]
        private EventSystem _eventSystem;

        void Awake()
        {
            var eventSystems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);

            if (eventSystems.Length > 1)
            {
                _eventSystem.enabled = false;
            }
        }
    }
}

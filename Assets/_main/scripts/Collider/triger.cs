using System;
using UnityEngine;
using UnityEngine.Events;

namespace Pixelcrew.component
{
    public class triger : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private  TrigerEvent _action;
        [SerializeField] private  TrigerEvent exitAction;

        private void OnTriggerEnter(Collider other)
        {
            if (_tag == "")
            {
                _action?.Invoke(other.gameObject);
            }
            else
            {
            

                if (other.gameObject.CompareTag(_tag))
                {

                    _action?.Invoke(other.gameObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(_tag))
            { 
                exitAction?.Invoke(other.gameObject);
            }
        }

        [Serializable]
        public class TrigerEvent : UnityEvent<GameObject>
        {
            
        }

        
    }
    
}


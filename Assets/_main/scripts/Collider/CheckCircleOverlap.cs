using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Pixelcrew.component
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private string _tags;
        [SerializeField] private Color _color;
        [SerializeField] private OnOverlapEvent _onOverlap;
        
        private readonly Collider2D[] _interectionResult = new Collider2D[10];
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = _color;;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif

        public void Check()
        {
            

            var size = Physics2D.OverlapCircleNonAlloc
            (
                transform.position,
                _radius,
                _interectionResult,
                _mask
                
            );
            var overlaps = new List<GameObject>();
            for (var i =0 ; i< size; i++)
            {
                var overlapResult = _interectionResult[i];
                
               
                if (_tags == overlapResult.tag)
                {
                    _onOverlap?.Invoke(overlapResult.gameObject);
                }
                
            }

          
        }
        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {
            
        }
    }
}

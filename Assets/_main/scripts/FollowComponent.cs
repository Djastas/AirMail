using System;
using UnityEngine;

namespace djastas.Scripts.Components
{
    [ExecuteAlways]
    public class FollowComponent : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        [Range(0,1)]
        [SerializeField] private float speed;

        [Header("position")] 
        [SerializeField] private bool xPos;
        [SerializeField] private bool yPos;
        [SerializeField] private bool zPos;
        
        [SerializeField] private Vector3 posOffset;
        [Header("Rotation")]
        
        [SerializeField] private bool xRot;
        [SerializeField] private bool yRot;
        [SerializeField] private bool zRot;
#if UNITY_EDITOR
        [SerializeField] private bool runOnEditor;
#endif

        
        private void Update()
        {
#if UNITY_EDITOR
            if (!runOnEditor) return;
#endif
            
            var targetPos = target.position;
            var selfPos = gameObject.transform.position;


            var idealPosX = xPos ? Mathf.Lerp(selfPos.x, targetPos.x, speed) : selfPos.x;
            var idealPosY = yPos ? Mathf.Lerp(selfPos.y, targetPos.y, speed) : selfPos.y;
            var idealPosZ = zPos ? Mathf.Lerp(selfPos.z, targetPos.z, speed) : selfPos.z;
            
            gameObject.transform.position = new Vector3(idealPosX,idealPosY,idealPosZ) + posOffset;
            
            
            var targetRot = target.rotation.eulerAngles;
            var selfRot = gameObject.transform.rotation.eulerAngles;
            
            var idealRotX = xRot ? Mathf.Lerp(selfRot.x, targetRot.x, speed) : selfRot.x;
            var idealRotY = yRot ? Mathf.Lerp(selfRot.y, targetRot.y, speed) : selfRot.y;
            var idealRotZ = zRot ? Mathf.Lerp(selfRot.z, targetRot.z, speed) : selfRot.z;

            
            gameObject.transform.rotation = Quaternion.Euler(idealRotX, idealRotY, idealRotZ);

        }
    }
}

using System;
using UnityEngine;

namespace _main.scripts
{
    public class BasicAirplaneController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [Range(-1, 1)] public float yaw;
        [Range(-1, 1)] public float up;
        private void FixedUpdate()
        {
            transform.position = transform.position + (transform.forward * speed);
            var x = transform.eulerAngles.x + up;
            var y = transform.eulerAngles.y + yaw;
            var z = transform.eulerAngles.z;
            
            
            transform.rotation = Quaternion.Euler(x, y, z);
        }

        public void UpdateInput(float pitch, float roll, int i)
        {
            yaw = roll;
            up = pitch;
        }
    }
}

using System;
using UnityEngine;

public class AirplaneInputController : MonoBehaviour
{
    [SerializeField] private AirplaneController airplaneController;
    [SerializeField] private Transform center;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform vrGlass;

    [Header("Roll")] 
    [SerializeField] private float rollSensitivity;
    [SerializeField] private float rollDeadZone;
    
    [Header("Pitch")] 
    [SerializeField] private float pitchSensitivity;
    [SerializeField] private float pitchDeadZone;
       private void Update()
       {
             //высчитываем поворот влево право roll
           var roll = (leftHand.localPosition.y - rightHand.localPosition.y) * rollSensitivity;
           if (Math.Abs(roll - 0) < rollDeadZone)
           {
               roll = 0;
           }
      
           var pitch = center.localPosition.y - ((leftHand.localPosition.y + rightHand.localPosition.y) / 2) * pitchSensitivity;
      
           if (Math.Abs(pitch - 0) < pitchDeadZone)
           {
               pitch = 0;
           }
           
           
           
           
           
           airplaneController.UpdateInput(pitch,roll,0);
       }
}

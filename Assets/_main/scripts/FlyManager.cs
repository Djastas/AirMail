using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.Player;
using UnityEngine;
using UnityEngine.Events;

public class FlyManager : MonoBehaviour
{
   [SerializeField] private HVRPlayerController _controller;
    
    private bool _isFly;
    [SerializeField] private UnityEvent onStartFly;
    [SerializeField] private UnityEvent onEndFly;
    private void Update()
    {
        if (_controller.Inputs.RightController.GripButton && _controller.Inputs.LeftController.GripButton)
        {
            SwitchFly();
        }
    }
[ContextMenu("TEST")]
    public void SwitchFly()
    {
        if (_isFly)
        {
            onEndFly.Invoke();
            _isFly = false;
        }
        else
        {
            onStartFly.Invoke();
            _isFly = true;
        }
    }
}

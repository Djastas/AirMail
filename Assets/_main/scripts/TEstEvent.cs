using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TEstEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent TEST;

    [ContextMenu("TEST")]
    private void Test()
    {
        TEST?.Invoke();
    }

}

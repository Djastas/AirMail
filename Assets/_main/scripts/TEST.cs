using System;
using UnityEngine;

public class TEST : MonoBehaviour
{
    private GameSession _session;
    
  [ContextMenu("test")]
  private void Test()
  {
    Debug.Log(_session.Data.Inventory.Count("Mail"));
  }
  private void Start()
  {
    _session = FindObjectOfType<GameSession>();
    
  }
}

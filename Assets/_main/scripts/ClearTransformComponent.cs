using System;
using UnityEngine;

public class ClearTransformComponent : MonoBehaviour
{
   public bool isUpdate;
   public void ClearTransform()
   {
      transform.localPosition = new Vector3(0, 0, 0);
   }

   private void Update()
   {
      if (isUpdate)
      {
         ClearTransform();

      }
   }

   public void IsUpdate()
   {
      isUpdate = !isUpdate;
   }
}

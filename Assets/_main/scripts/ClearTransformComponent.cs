using UnityEngine;

public class ClearTransformComponent : MonoBehaviour
{
   public void ClearTransform()
   {
      transform.localPosition = new Vector3(0, 0, 0);
   }
}

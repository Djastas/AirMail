using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField] private float windForce;
    [SerializeField] private float lengthGizmo;

    void OnTriggerStay (Collider  other)
    {

        Rigidbody  rb = other.GetComponent<Rigidbody >();
        
        if (rb != null)
        {
            // Convert the object's rotation to a Vector3 direction
            Vector3 windDirection = Quaternion.Euler( transform.eulerAngles.x,  transform.eulerAngles.y, transform.eulerAngles.z) * Vector3.right;
            rb.AddForce(windDirection * windForce);
            Debug.Log("windZone is active");
        }
        else
        { 
            rb = other.GetComponentInParent<Rigidbody >();
            if (rb != null)
            {
                Vector3 windDirection = Quaternion.Euler( transform.eulerAngles.x,  transform.eulerAngles.y, transform.eulerAngles.z) * Vector3.right;
                rb.AddForce(windDirection * windForce);
                Debug.Log("windZone is active in parent");
            }
            else
            {
                Debug.Log("windZone not work");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 windDirection = Quaternion.Euler( transform.eulerAngles.x,  transform.eulerAngles.y, transform.eulerAngles.z) * Vector3.right;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + windDirection * lengthGizmo;
        Gizmos.DrawLine(startPos, endPos);
    }
}
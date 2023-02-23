using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackFlowHandler : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerIdentifier.IsPlayer(other))
        {
            Debug.Log(transform.forward);
            Debug.Log(other.gameObject.transform.forward);
            // If this angle in bigger than 90 degrees we are runnig to wrong flow
            Debug.Log(Vector3.Angle(other.gameObject.transform.forward, transform.forward));
        }
    }
}

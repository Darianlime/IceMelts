using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePlaneCollider : MonoBehaviour
{
    // Start is called before the first frame update
    Ray ray;
    public GameObject planeCollider;
    public float height = 0;
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.up * height, Color.red);
    }

    public void PlacePlaneCollider() {
        ray = new Ray(transform.position, transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.collider.CompareTag("Roof")) {
                planeCollider.transform.position = new Vector3(gameObject.transform.position.x, hit.transform.position.y - (hit.transform.localScale.y/2), gameObject.transform.position.z);
            }
        }
    }

}

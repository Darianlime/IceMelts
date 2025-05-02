using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCollider : MonoBehaviour
{
    public CloneScript clonescript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        clonescript.collisionTriggered = true;        

        // Positioning the clones near the collision point
        clonescript.activeClone.transform.position = collision.transform.position + new Vector3(0, 1, 0);
    }
}

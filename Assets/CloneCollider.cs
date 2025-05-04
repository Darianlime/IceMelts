using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneCollider : MonoBehaviour
{
    public CloneScript clonescript;

    private void OnCollisionEnter(Collision collision) {

        // Positioning the clones near the collision point
        if (collision.gameObject.CompareTag("Player")) {
            clonescript.collisionTriggered = true;        
            clonescript.playerClonePrefab.transform.position = collision.transform.position + new Vector3(0, 0, 0);
            clonescript.Clone();
        }
    }

    private void OnCollisionStay(Collision collision) {
        clonescript.collisionTriggered = true;        
    }
}

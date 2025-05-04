using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MergeCollider : MonoBehaviour
{
    public MergeScript mergescript;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            mergescript.MergeCollisionTriggered = true;
            mergescript.Merge();
        }
    }
}
// on collision whatnot
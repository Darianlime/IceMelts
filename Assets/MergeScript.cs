using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeScript : MonoBehaviour
{
    //public PhaseChange phasechange;
    public CloneScript clonescript;
    public bool MergeCollisionTriggered = false;
    // Start is called before the first frame update

    public void Merge() {
        if (MergeCollisionTriggered == true) {
            clonescript.playerClonePrefab.SetActive(false);
            
            clonescript.isDone = false;
            clonescript.collisionTriggered = false;
        }
    }
}


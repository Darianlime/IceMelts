using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeScript : MonoBehaviour
{
    public PhaseChange phasechange;
    public CloneScript clonescript;
    public bool MergeCollisionTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MergeCollisionTriggered == true) {
            //Destroy(clonescript.activeClone);
            clonescript.playerClonePrefab.SetActive(false);
            
            phasechange.cloneTriggered = false;
            clonescript.isDone = false;
            clonescript.collisionTriggered = false;
        }
    }
}


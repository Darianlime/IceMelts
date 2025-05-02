using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public PhaseChange phasechange;
    public MergeScript mergescript;


    private GameObject cloneBlock;
    public GameObject playerClonePrefab;
    public GameObject activeClone;
    public bool isDone = false;
    public bool collisionTriggered = false;


    void Start()
    {
        playerClonePrefab = GameObject.Find("Player");
    }
   
    // Update is called once per frame
    void Update()
    {
        if (collisionTriggered == true) {
            if (!isDone) {
                // Destroy the previous clone if it exists
                if (activeClone != null) {
                    Destroy(activeClone);
                }

                // Create a new clone
                activeClone = Instantiate(playerClonePrefab, playerClonePrefab.transform.position, Quaternion.identity);
                isDone = true;
            }

            phasechange.cloneTriggered = true;
            collisionTriggered = false;
            mergescript.MergeCollisionTriggered = false;
            isDone = true;
        }
    }
}


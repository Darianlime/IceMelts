using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour
{
    // Start is called before the first frame update
    //public PhaseChange phasechange;
    public MergeScript mergescript;
    public GameObject playerClonePrefab;
    public GameObject activeClone = null;
    public bool isDone = false;
    public bool collisionTriggered = false;


    void Awake()
    {
        playerClonePrefab = Instantiate(playerClonePrefab, new Vector3(999,999,0), Quaternion.identity);
    }

    public void Clone() {
        if (collisionTriggered == true) {
            if (!isDone) {
                playerClonePrefab.SetActive(true);
                isDone = true;
            }

            collisionTriggered = false;
            mergescript.MergeCollisionTriggered = false;
            isDone = true;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCollider : MonoBehaviour
{
    public MergeScript mergescript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        mergescript.MergeCollisionTriggered = true;
    }
}
// on collision whatnot
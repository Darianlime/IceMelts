using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public bool isInBox;
    private Rigidbody playerBody;
    private Quaternion fanRot;
    [SerializeField] public float fanSpeed = 100;
    void Start()
    {
        fanRot = transform.parent.gameObject.transform.rotation;
    }

    void Update(){
        if(isInBox){
            if (playerBody != null) {
                Vector3 force = new Vector3(-1 * fanSpeed, 0, 0);
                playerBody.AddForce(fanRot * force);
            }
        }
    }

    void OnTriggerStay(Collider other){
        if(other.tag.Equals("Player")) {
            isInBox = true;
            if (playerBody == null) {
                playerBody = other.attachedRigidbody;
            }
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag == "Player") {
            isInBox = false;
        }
    }
}

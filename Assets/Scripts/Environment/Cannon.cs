using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public bool isInBox;
    private Rigidbody playerBody;
    private Quaternion cannonRot;
    [SerializeField] public float shotTime = 3.00f;
    [SerializeField] public float shotForce = 10000;    // Add your Audi Clip Here
    public float targetTime;
    private AudioSource audioSource;
    public bool shot = false;

    void Start()
    {
        cannonRot = transform.parent.gameObject.transform.rotation;
        targetTime = shotTime;
        // Attempt to get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // If no AudioSource is found, add one dynamically
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update(){
        if(isInBox){
            if (targetTime > 0) {
                targetTime -= Time.deltaTime;
            }
            else {
                if (playerBody != null) {
                    if (shot == false) {
                        audioSource.Play();
                        shot = true;
                        Vector3 force = new Vector3(0, shotForce, 0);
                        playerBody.AddForce(cannonRot * force);
                    }
                }
            }
        }
    }

    void OnTriggerStay(Collider other){
        if(other.tag == "Player") {
            isInBox = true;
            if (playerBody == null) {
                playerBody = other.attachedRigidbody;
            }
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag == "Player") {
            isInBox = false;
            targetTime = shotTime;
            shot = false;
        }
    }
}

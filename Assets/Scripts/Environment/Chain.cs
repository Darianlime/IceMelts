using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public AudioSource rattling;
    public Boolean playSound = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        if(other.tag == "Player") {
            //Debug.Log("entered the area");
            if (playSound == false) {
                rattling.Play();
                playSound = true;
            }
        }
    }
    void OnTriggerExit(Collider other){
        if(other.tag == "Player") {
            //Debug.Log("left the area");
            rattling.Play();
            playSound = false;
        }
    }
}

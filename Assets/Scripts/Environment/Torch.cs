using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public Boolean extinguished = false;
    public event Action off;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (extinguished) {
            GetComponent<Renderer>().enabled = false;
        }
        
    }
    void OnTriggerStay(Collider other){
        if(other.tag == "Player") {
            if (!extinguished) {
                Debug.Log("extinguished");
                extinguished = true;
                off.Invoke();
            }
        }
    }
}

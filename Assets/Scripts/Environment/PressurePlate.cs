using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Boolean pressed = false;
    public event Action activate;
    public event Action deactivate;
    public Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.parent.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other){
        if(other.tag == "Player") {
            if (!pressed) {
                pressed = true;
                activate.Invoke();
                Vector3 newPos = originalPos;
                newPos.y = originalPos.y - 0.30f;
                transform.parent.gameObject.transform.position = newPos;
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player") {
            pressed = false;
            deactivate.Invoke();
            transform.parent.gameObject.transform.position = originalPos;
        }
    }
}

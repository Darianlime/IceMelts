using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Boolean pressed = false;
    private Boolean on = false;
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
                Debug.Log("pressed");
                pressed = true;
                Vector3 newPos = originalPos;
                if (!on) {
                    
                    activate.Invoke();
                    on = true;
                    newPos.y = originalPos.y - 0.30f;
                    transform.parent.gameObject.transform.position = newPos;
                }
                else {
                    deactivate.Invoke();
                    on = false;
                    newPos.y = originalPos.y - 0.30f;
                    transform.parent.gameObject.transform.position = newPos;
                }
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Player") {
            Debug.Log("unpressed");
            pressed = false;
            transform.parent.gameObject.transform.position = originalPos;

        }
    }
}

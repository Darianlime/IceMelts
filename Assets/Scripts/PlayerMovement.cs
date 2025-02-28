using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int form = 0;
    [SerializeField] private float speed = 5;
    [SerializeField] private float floatSpeed = 5;
    [SerializeField] private float waterSpeedReduction = 0.8f;
    int layerWater = 4;
    int layerDefault = 0;

    //[SerializeField] lets you adjust and see values in the inspector
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        Debug.Log("Current layer: " + this.gameObject.layer);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b")) {
            form = 1;
            body.mass = 1;
            this.gameObject.layer = layerWater;
            Debug.Log("Current layer: " + this.gameObject.layer);
        }
        if (Input.GetKeyDown("v")) {
            form = 0;
            body.mass = 10;
            this.gameObject.layer = layerDefault;
            Debug.Log("Current layer: " + this.gameObject.layer);
        }
        if (Input.GetKeyDown("n")) {
            form = 2;
            body.mass = 15;
            this.gameObject.layer = layerWater;
            Debug.Log("Current layer: " + this.gameObject.layer);
        }
        if (form == 0) {
            body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, body.velocity.y, body.velocity.z);
        }
        else if (form == 1) {
            body.velocity = new Vector3(body.velocity.x, floatSpeed, body.velocity.z);
        }
        else if (form == 2) {
            body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed  * waterSpeedReduction, body.velocity.y, body.velocity.z);
        }
    }
}

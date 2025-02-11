using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int form = 0;
    [SerializeField] private float speed = 5;
    [SerializeField] private float floatSpeed = 5;

    //[SerializeField] lets you adjust and see values in the inspector
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b")) {
            form = 1;
            body.mass = 1;
        }
        if (Input.GetKeyDown("v")) {
            form = 0;
            body.mass = 10;
        }
        if (form == 0) {
            body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, body.velocity.y, body.velocity.z);
        }
        else if (form == 1) {
            body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, floatSpeed, body.velocity.z);
        }
    }
}

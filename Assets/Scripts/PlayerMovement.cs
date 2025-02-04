using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5;
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
        body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, body.velocity.y, body.velocity.z);
    }
}

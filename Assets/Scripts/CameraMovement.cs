using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    private float zoom = 10;
    private Boolean zoomedOut = false;
    [SerializeField] private float maxZoom = 50;
    [SerializeField] private float minZoom = 10;
    [SerializeField] private float cameraZoomSpeed = 0.1f;
    GameObject Player;
    Vector3 camOffset;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        camOffset = gameObject.transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = gameObject.transform.position;
        Vector3 target = current;
        if (Player != null) {
            target = Player.transform.position + camOffset;
        }
        gameObject.transform.position = current + (target - current) * 0.4f;
        camOffset.z = -zoom;
        if (Input.GetKeyDown("c")) {
            if (zoomedOut) {
                zoomedOut = false;
            }
            else {
                zoomedOut = true;
            }
        }
        if (zoomedOut) {
            if (zoom < maxZoom) {
                zoom += cameraZoomSpeed;
            }
        }
        else {
            if (zoom > minZoom){
                zoom -= cameraZoomSpeed;
            }
        }
    }
}

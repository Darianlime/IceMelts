using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 originalPos;
    [SerializeField] public Vector3 nextPos;
    [SerializeField] public Torch torch;
    [SerializeField] public PressurePlate plate;
    public Boolean on = false;
    public float alpha = 0f;
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        if (torch != null) {
            torch.off += nextMove;
        }
        if (plate != null) {
            plate.activate += nextMove;
            plate.deactivate += moveBack;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (on) {
            transform.position = Vector3.Lerp(originalPos, nextPos, alpha);
            if (alpha < 1) {
                alpha += Time.deltaTime;
            }
        }
        else {
            transform.position = Vector3.Lerp(originalPos, nextPos, alpha);
            if (alpha > 0) {
                alpha -= Time.deltaTime;
            }
        }
    }
    public void nextMove() {
        on = true;
        //Debug.Log("player on");
    }
    public void moveBack() {
        on = false;
        //Debug.Log("off");
    }
}

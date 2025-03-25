using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource iceToWater;
    public AudioSource iceToSteam;
    public AudioSource waterToIce;
    public AudioSource waterToSteam;
    public AudioSource steamToIce;
    public AudioSource steamToWater;
    [SerializeField] private int form = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k")) {
            if (form == 0) {
                iceToSteam.Play();
            }
            else if (form == 2) {
                waterToSteam.Play();
            }
            else {
                //Nothing
            }
            form = 1;
        }
        if (Input.GetKeyDown("l")) {
            if (form == 1) {
                steamToIce.Play();
            }
            else if (form == 2) {
                waterToIce.Play();
            }
            else {
                //Nothing
            }
            form = 0;
        }
        if (Input.GetKeyDown("j")) {
            if (form == 0) {
                iceToWater.Play();
            }
            else if (form == 1) {
                steamToWater.Play();
            }
            else {
                //Nothing
            }
            form = 2;
        }
    }
}

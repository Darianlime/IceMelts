using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChange : MonoBehaviour
{
    // player 
    public GameObject Player;
    public GameObject Player_Ice;
    public GameObject Player_Water;
    public GameObject Player_Steam;
    public bool phase1Active = true;
    public bool phase2Active = false;
    public bool phase3Active = false;

    public bool cloneTriggered = false;

    void Start() 
    {
        Player_Ice.SetActive(true);
        Player_Water.SetActive(false);
        Player_Steam.SetActive(false);
    }
    void Update()
    {   

        if (Input.GetKeyDown(KeyCode.L) || phase3Active == true) // (water)
        {
            phase3Active = true;
            phase1Active = false;
            phase2Active = false;
            Player_Ice.SetActive(false);
            Player_Water.SetActive(true);
            Player_Steam.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.K) || phase2Active == true) // (steam)
        {
            phase2Active = true;
            phase1Active = false;
            phase3Active = false;
            Player_Ice.SetActive(false);
            Player_Water.SetActive(false);
            Player_Steam.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.J) || phase1Active == true) // (ice)
        {
            phase1Active = true;
            phase2Active = false;
            phase3Active = false;
            Player_Ice.SetActive(true);
            Player_Water.SetActive(false);
            Player_Steam.SetActive(false);
        }
    }
}

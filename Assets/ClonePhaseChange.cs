using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePhaseChange : MonoBehaviour
{
    //main character
    public GameObject Player_Clone;
    public GameObject Player_Ice_Clone;
    public GameObject Player_Water_Clone;
    public GameObject Player_Steam_Clone;
    public bool phase1Active = true;
    public bool phase2Active = false;
    public bool phase3Active = false;

    // clone & merge triggers
    public Rigidbody rb;
    public BoxCollider bc; 
    public bool cloneTriggered = false;
    
    void Start() 
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();

        rb.constraints = RigidbodyConstraints.FreezeAll;
        bc.enabled = false;
        
        Player_Ice_Clone.SetActive(false);
        Player_Water_Clone.SetActive(false);
        Player_Steam_Clone.SetActive(false);
    }
    void Update()
    {   
        // cloningx
        if (cloneTriggered)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            bc.enabled = true;
        }
        else // merging
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            bc.enabled = false;

            Player_Ice_Clone.SetActive(false);
            Player_Water_Clone.SetActive(false);
            Player_Steam_Clone.SetActive(false);
            phase1Active = false;
            phase2Active = false;
            phase3Active = false;
        }

    
        if (Input.GetKeyDown(KeyCode.L)) // (water)
        {
            phase3Active = true;
            phase1Active = false;
            phase2Active = false;
            Player_Ice_Clone.SetActive(false);
            Player_Water_Clone.SetActive(true);
            Player_Steam_Clone.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.K)) // (steam)
        {
            phase2Active = true;
            phase1Active = false;
            phase3Active = false;
            Player_Ice_Clone.SetActive(false);
            Player_Water_Clone.SetActive(false);
            Player_Steam_Clone.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.J)) // (ice)
        {
            phase1Active = true;
            phase2Active = false;
            phase3Active = false;
            Player_Ice_Clone.SetActive(true);
            Player_Water_Clone.SetActive(false);
            Player_Steam_Clone.SetActive(false);
        }
    }

    

}

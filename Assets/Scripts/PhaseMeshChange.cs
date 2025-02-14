using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMesh : MonoBehaviour
{
    [SerializeField] private MeshFilter meshYouWantToChange;
    [SerializeField] private Mesh[] meshYouWantToUse;

    private int currentMesh;

    void Update()
    { 
        // if (Input.GetKeyDown(KeyCode.Space)) 
        // {
        //     meshYouWantToChange.mesh = meshYouWantToUse[currentMesh];
        //     currentMesh++; // cycles mesh
        //     if (currentMesh >= meshYouWantToUse.Length) // resets to start of cycle
        //     {
        //         currentMesh = 0;
        //     }
            
        // }
        
        if (Input.GetKeyDown(KeyCode.J)) // Left arrow (ice)
        {
            meshYouWantToChange.mesh = meshYouWantToUse[0];
        }
        else if (Input.GetKeyDown(KeyCode.K)) // Up arrow (steam)
        {
            meshYouWantToChange.mesh = meshYouWantToUse[1];
        }
        else if (Input.GetKeyDown(KeyCode.L)) // Right arrow (water)
        {
            meshYouWantToChange.mesh = meshYouWantToUse[2];

        }
    }
    
    // public void ChangeMeshWithButton()
    // {
    //     meshYouWantToChange.mesh = moeshYouWantToUse[currentMesh];
    //     currentMesh++;
    // }
}

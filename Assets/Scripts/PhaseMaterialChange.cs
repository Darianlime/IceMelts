using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] private MeshRenderer materialYouWantToChange;
    [SerializeField] private Material[] materialYouWantToUse;

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.J)) // Left arrow (ice)
        {
            materialYouWantToChange.material = materialYouWantToUse[0];
        }
        else if (Input.GetKeyDown(KeyCode.K)) // Up arrow (steam)
        {
            materialYouWantToChange.material = materialYouWantToUse[1];
        }
        else if (Input.GetKeyDown(KeyCode.L)) // Right arrow (water)
        {
            materialYouWantToChange.material = materialYouWantToUse[2];

        }
    }
}
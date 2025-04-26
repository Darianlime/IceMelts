using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofLayering : MonoBehaviour
{
    private List<GameObject> roofList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++) {
            this.gameObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Roof");
        }
    }
}

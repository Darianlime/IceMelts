using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstat : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPefab;
    void Start()
    {
        Instantiate(playerPefab, new Vector3(0,0,0), Quaternion.identity);
    }

    // Update is called once per frame
}

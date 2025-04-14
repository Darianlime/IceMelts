using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public int key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider other){
        if(other.tag == "Player") {
            player.keys[key] = true;
            transform.position = new Vector3(10000, 1000000, 1000000);
        }
    }
}

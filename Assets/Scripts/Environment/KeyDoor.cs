using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] public Player player;
    public int key = 0;
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
            Debug.Log("touched");
            Debug.Log(player.keys);
            if (player.keys > 0) {
                player.keys--;
                Debug.Log(player.keys);
                transform.parent.gameObject.transform.position = new Vector3(10000, 1000000, 1000000);
            }
        }
    }
}

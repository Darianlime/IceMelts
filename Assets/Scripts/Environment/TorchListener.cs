using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchListener : MonoBehaviour
{
    [SerializeField] public Torch torch;
    // Start is called before the first frame update
    void Start()
    {
        torch.off += doSomething;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doSomething() {
        Debug.Log("Did Something");
    }
}

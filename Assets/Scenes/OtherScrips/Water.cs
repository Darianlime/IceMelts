using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animatorWater;
    public MeshRenderer meshRenderer;
    private float startTime = 0f;
    void Awake()
    {
        animatorWater = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void WaterActive() {
        float morph = meshRenderer.material.GetFloat("_Morph");
        if (morph == 1) {
            meshRenderer.enabled = false;
        } 
        if (morph == 0) {
            meshRenderer.enabled = true;
        }
    }

    public void GrowWater() {
        startTime = animatorWater.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (startTime > 1) {
            startTime = 1;
        }
        animatorWater.CrossFadeInFixedTime("WaterGrow", 0.5f, 0, 1 - startTime); 
    }

    public void DissolveWater() {
        if (startTime != 0) {
            startTime = animatorWater.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
        if (startTime > 1) {
            startTime = 0;
        }
        animatorWater.CrossFadeInFixedTime("WaterDissolve", 0.5f, 0, startTime);
    }

    private void OnDestroy()
    {
      
    }
}

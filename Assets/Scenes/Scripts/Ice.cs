using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public Animator animatorOutside;
    public Animator animatorInside;
    public BoxCollider boxCollider;
    public float x = 7.5f;
    public float y = 0.5f;
    public float yCenter = 0.23f;
    public SkinnedMeshRenderer meshRendererOutside;
    public SkinnedMeshRenderer meshRendererInside;
    private float startTime = 0f;
    private Ray ray;
    public float maxRayDistance = 0.5f;
    private int layerMask;
    private void Awake()
    {
        AnimationActions.current.IceGrow += GrowIce;
        AnimationActions.current.IceDissolve += DissolveIce;
        AnimationActions.current.IceMelt += IceToWater;
        AnimationActions.current.WaterToIce += WaterToIce;
        AnimationActions.current.IceCollider += IceCollider;
        AnimationActions.current.WaterCollider += WaterCollider;
    }

    private void Start() {
        boxCollider = GetComponent<BoxCollider>();
        layerMask = 1 << LayerMask.NameToLayer("Roof");
    }

    public void Update()
    {
        RayCastFloor();
        Debug.DrawLine(this.transform.position + new Vector3(0, 1, 0), Vector3.down*maxRayDistance + this.transform.position + new Vector3(0, 1, 0), Color.red);  
    }

    public void RayCastFloor() {
        ray = new Ray(this.transform.position + new Vector3(0, 1, 0), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, layerMask)) {
            if (hit.collider != null) {
                animatorOutside.SetBool("isFallingDone", true);
                if (animatorOutside.GetCurrentAnimatorStateInfo(0).IsName("IceToWaterOutside")) {
                    WaterCollider();
                }
            } 
        } else {
            animatorOutside.SetBool("isFallingDone", false);
            if (animatorOutside.GetCurrentAnimatorStateInfo(0).IsName("IceToWaterFalling")) {
                IceCollider();
            }
        }
    }

    public void IceCollider() {
        boxCollider.size = new Vector3(2.1f, 2.1f, 2.1f);
        boxCollider.center = new Vector3(0, 1.05f, 0);
    }

    public void WaterCollider() {
        boxCollider.size = new Vector3(6f, 0.5f, 2.1f);
        boxCollider.center = new Vector3(0, 0.23f, 0);
    }

    public void GrowIce() {
        //animatorOutside.SetBool("isFallingDone", false);
        animatorOutside.CrossFade("IceGrowOutside", 0.5f, 0);
        animatorInside.CrossFade("IceGrowInside", 0.5f, 0);
    }

    public void DissolveIce() {
        //animatorOutside.SetBool("isFallingDone", false);
        animatorOutside.CrossFade("IceDissolveOutside", 0.5f, 0);
        animatorInside.CrossFade("IceDissolveInside", 0.5f, 0);
    }

    public void IceToWater() {
        if (!animatorOutside.GetBool("isFallingDone")) {
            //animatorOutside.SetBool("isFallingDone", false);
            animatorInside.CrossFade("IceDissolveInsideRev", 0.2f, 0);
            animatorOutside.CrossFade("IceToWaterFalling", 0.2f, 0);
        } else if (animatorOutside.GetBool("isFallingDone")) {
            animatorInside.CrossFade("IceToWaterInside", 0.2f, 0);
            animatorOutside.CrossFade("IceToWaterOutside", 0.2f, 0);
        }
    }

    public void WaterToIce() {
        //animatorOutside.SetBool("isFallingDone", false);
        if (animatorOutside.GetCurrentAnimatorStateInfo(0).IsName("IceToWaterFalling")) {
            startTime = animatorOutside.GetCurrentAnimatorStateInfo(0).normalizedTime;
            animatorInside.CrossFade("IceGrowInsideRev", 0.2f, 0, 0);
            animatorOutside.CrossFade("WaterToIceFalling", 0.2f, 0, 0);
        } else {
            animatorInside.CrossFade("WaterToIceInside", 0.2f, 0, 0);
            animatorOutside.CrossFade("WaterToIceOutside", 0.2f, 0);
        }
    }

    private void OnDisable() 
    {
        AnimationActions.current.IceGrow -= GrowIce;
        AnimationActions.current.IceDissolve -= DissolveIce;
        AnimationActions.current.IceMelt -= IceToWater;
        AnimationActions.current.WaterToIce -= WaterToIce;
        AnimationActions.current.IceCollider -= IceCollider;
        AnimationActions.current.WaterCollider -= WaterCollider;
    }
}

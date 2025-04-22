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
    }

    public void IceCollider() {
        boxCollider.size = new Vector3(2.1f, 2.1f, 2.1f);
        boxCollider.center = new Vector3(0, 1.05f, 0);
    }

    public void WaterCollider() {
        boxCollider.size = new Vector3(7.5f, 0.5f, 2.1f);
        boxCollider.center = new Vector3(0, 0.23f, 0);
    }

    public void GrowIce() {
        animatorOutside.CrossFade("IceGrowOutside", 0.5f, 0);
        animatorInside.CrossFade("IceGrowInside", 0.5f, 0);
    }

    public void DissolveIce() {
        animatorInside.SetBool("GrowingContinuing", false);
        animatorOutside.SetBool("GrowingContinuing", false);
        animatorOutside.CrossFade("IceDissolveOutside", 0.5f, 0);
        animatorInside.CrossFade("IceDissolveInside", 0.5f, 0);
    }

    public void IceToWater() {
        animatorInside.SetBool("GrowingContinuing", false);
        animatorOutside.SetBool("GrowingContinuing", false);
        if (animatorInside.GetCurrentAnimatorStateInfo(0).IsName("IceDissolveInside")) {
            animatorInside.CrossFade("IceGrowInsideHelper", 0.2f, 0);
            animatorOutside.CrossFade("IceGrowOutsideHelper", 0.2f, 0);
        } else {
            animatorInside.CrossFade("IceToWaterInside", 0.2f, 0);
            animatorOutside.CrossFade("IceToWaterOutside", 0.2f, 0);
        }
    }

    public void WaterToIce() {
        if (!animatorInside.GetCurrentAnimatorStateInfo(0).IsName("IceToWaterInside")) {
            startTime = animatorInside.GetCurrentAnimatorStateInfo(0).normalizedTime;
            animatorInside.CrossFade("IceGrowInsideHelper", 0.2f, 0, startTime);
            animatorOutside.CrossFade("IceGrowOutsideHelper", 0.2f, 0, startTime);
            animatorInside.SetBool("GrowingContinuing", true);
            animatorOutside.SetBool("GrowingContinuing", true);
        } else {
            animatorInside.CrossFade("WaterToIceInside", 0.2f, 0);
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

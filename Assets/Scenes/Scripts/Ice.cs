using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Ice : MonoBehaviour
{

    public Animator animatorIce;
    public Animator animatorMeltOutside;
    public Animator animatorMeltInside;
    public SkinnedMeshRenderer meshRendererOutside;
    public SkinnedMeshRenderer meshRendererInside;

    private float startTime = 0f;
    private void Awake()
    {
        animatorIce = GetComponent<Animator>();
        AnimationActions.current.IceGrow += GrowIce;
        AnimationActions.current.IceDissolve += DissolveIce;
        AnimationActions.current.IceMelt += MeltIce;
        AnimationActions.current.WaterFreeze += FreezeWater;
    }

    public void GrowIce() {
        startTime = animatorIce.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (startTime > 1) {
            startTime = 1;
        }
        animatorIce.CrossFadeInFixedTime("IceGrow", 0.5f, 0, 1 - startTime);
    }

    public void DissolveIce() {
        if (startTime != 0) {
            startTime = animatorIce.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
        if (startTime > 1) {
            startTime = 0;
        }
        animatorIce.CrossFadeInFixedTime("IceDissolve", 0.5f, 0, startTime);
    }

    public void MeltIce() {
        // if (startTime != 0) {
        //     startTime = animatorMeltOutside.GetCurrentAnimatorStateInfo(0).normalizedTime;
        // }
        // if (startTime > 1) {
        //     startTime = 0;
        // }
        animatorMeltOutside.SetBool(Animator.StringToHash("isPressed"), false);
        animatorMeltOutside.CrossFadeInFixedTime("IceToWaterOutside", 0.3f, 0, 0);
        animatorMeltInside.SetBool(Animator.StringToHash("isPressed"), false);
        animatorMeltInside.CrossFadeInFixedTime("IceToWaterInside", 0.3f, 0, 0);
        animatorIce.SetBool(Animator.StringToHash("isPressed"), false);
        animatorIce.CrossFadeInFixedTime("IceDissolveInside", 0.1f, 0, 0);
    }

    public void FreezeWater() {
        animatorMeltOutside.SetBool(Animator.StringToHash("isPressed"), true);
        animatorMeltInside.SetBool(Animator.StringToHash("isPressed"), true);
        animatorIce.SetBool(Animator.StringToHash("isPressed"), true);
        meshRendererInside.material.SetFloat("_Morph", meshRendererInside.material.GetFloat("_Morph"));
    }

    private void OnDisable() 
    {
        AnimationActions.current.IceGrow -= GrowIce;
        AnimationActions.current.IceDissolve -= DissolveIce;
        AnimationActions.current.IceMelt -= MeltIce;
        AnimationActions.current.WaterFreeze -= FreezeWater;
    }
}

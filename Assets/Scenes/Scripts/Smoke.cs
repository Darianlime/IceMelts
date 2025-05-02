using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public ParticleSystem smokeParticle;
    public GameObject planeCollider;
    private bool stoppedSmoke = false;  
    // Start is called before the first frame update
    private void Awake() {
        smokeParticle.Stop();
        AnimationActions.current.SmokeStop += StopEmitting;
        AnimationActions.current.SmokeEmitt += Emitting;
    }
    public void Emitting() {
        if (!stoppedSmoke) {
            smokeParticle.Play();
            stoppedSmoke = true;
        }
    }

    public void StopEmitting() {
        if (stoppedSmoke) {
            smokeParticle.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            stoppedSmoke = false;
        }
    }

    private void OnDisable() {
        AnimationActions.current.SmokeEmitt -= Emitting;
        AnimationActions.current.SmokeStop -= StopEmitting;
    }
}


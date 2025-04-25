using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public ParticleSystem smokeParticle;
    public GameObject planeCollider;
    private Ray ray;
    private bool stoppedSmoke = false;  
    private string roofTag = "Roof";
    private int layerMask;
    // Start is called before the first frame update
    private void Awake() {
        smokeParticle.Stop();
        AnimationActions.current.SmokeStop += StopEmitting;
        AnimationActions.current.SmokeEmitt += Emitting;
        planeCollider = GameObject.CreatePrimitive(PrimitiveType.Plane);
        smokeParticle.collision.AddPlane(planeCollider.transform);
        layerMask = 1 << LayerMask.NameToLayer("Roof");
    }

    private void FixedUpdate()
    {
        PlacePlaneCollider();
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

    public void PlacePlaneCollider() {
        ray = new Ray(transform.parent.position, transform.parent.up);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) {
            if (hit.collider.CompareTag(roofTag)) {
                planeCollider.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y - (hit.transform.localScale.y/2) + 0.5f, hit.transform.position.z);
                planeCollider.transform.localScale = new Vector3(hit.transform.localScale.x/10, 1, hit.transform.localScale.z/10-0.05f);
                planeCollider.transform.rotation = Quaternion.Euler(180, 0, 0);
            }
        }
    }

    private void OnDisable() {
        AnimationActions.current.SmokeEmitt -= Emitting;
        AnimationActions.current.SmokeStop -= StopEmitting;
    }
}


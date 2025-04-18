using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {
    Ice,
    Smoke,
    Water
}

public class PlayerAnimationController : MonoBehaviour
{
    public SmokePlaneCollider smokePlaneCollider;
    public bool[] getButtonPress;
    private State oldState;
    
    private void Start()
    {
        smokePlaneCollider = GetComponent<SmokePlaneCollider>();
        oldState = State.Ice;
        getButtonPress = new bool[]
        {
            false, //Ice
            true,  //Water
            true   //Smoke
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && getButtonPress[0]) {
            ChangeAnimation(State.Ice);
        }
        if (Input.GetKeyDown(KeyCode.K) && getButtonPress[1]) {
            smokePlaneCollider.PlacePlaneCollider();
            ChangeAnimation(State.Smoke);
        }
        if (Input.GetKeyDown(KeyCode.J) && getButtonPress[2]) {
            ChangeAnimation(State.Water);
        }
    }

    public void ChangeAnimation(State newState) {
        switch (oldState) {
            case State.Ice:
                ChangeState(newState);
                getButtonPress[0] = true;
                break;
            case State.Smoke:
                ChangeState(newState);
                getButtonPress[1] = true;
                break;
            case State.Water:
                ChangeState(newState);
                getButtonPress[2] = true;
                break;
        }
    }

    public void ChangeState(State newState) {
        State state = AnimationActions.current.InvokeAnimation(oldState, newState);
        oldState = newState;
        getButtonPress[(int)state] = false;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class AnimationActions : MonoBehaviour
{
    public static AnimationActions current; 
    public event Action IceGrow;
    public event Action IceDissolve;
    public event Action IceMelt;
    public event Action WaterFreeze;
    public event Action SmokeEmitt;
    public event Action SmokeStop;
    public Dictionary<State, Dictionary<State, List<Action>>> iceAnimations = new();
    public Dictionary<State, Dictionary<State, List<Action>>> waterAnimations = new();
    public Dictionary<State, Dictionary<State, List<Action>>> smokeAnimations = new();
    Dictionary<State, List<Action>> iceStateAnim = new();
    Dictionary<State, List<Action>> waterStateAnim = new();
    Dictionary<State, List<Action>> smokeStateAnim = new();

    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        //Ice Actions
        iceStateAnim.Add(State.Water, new List<Action> { IceMelt });
        iceStateAnim.Add(State.Smoke, new List<Action> { SmokeEmitt, IceDissolve });
        iceAnimations.Add(State.Ice, iceStateAnim);

        //Water Actions
        waterStateAnim.Add(State.Ice, new List<Action> { WaterFreeze });
        waterStateAnim.Add(State.Smoke, new List<Action> { SmokeEmitt, IceDissolve, WaterFreeze});
        waterAnimations.Add(State.Water, waterStateAnim);
       
        //Smoke Actions
        smokeStateAnim.Add(State.Ice, new List<Action> { SmokeStop, IceGrow });
        smokeStateAnim.Add(State.Water, new List<Action> { SmokeStop, IceGrow, IceMelt});
        smokeAnimations.Add(State.Smoke, smokeStateAnim);
    }

    public Dictionary<State, List<Action>> GetDictAnimation(State oldState) {
        if (iceAnimations.ContainsKey(oldState)) {
            return iceAnimations[oldState];
        } else if (waterAnimations.ContainsKey(oldState)) {
            return waterAnimations[oldState];
        } else {
            return smokeAnimations[oldState];
        }
    }

    public State InvokeAnimation(State oldState, State newState) {
        List<Action> list = GetDictAnimation(oldState)[newState];
        print (oldState + " " + newState + " " + list[0]);
        for (int i = 0; i < list.Count; i++) {
            list[i].Invoke();
        }
        return GetDictAnimation(oldState).FirstOrDefault(x => x.Value == list).Key;
    }
}

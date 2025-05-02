using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class WaterBoxCollider : MonoBehaviour
{
    public BoxCollider box1;
    public BoxCollider box2;
    public BoxCollider box3;
    public BoxCollider box4;
    public Rigidbody rb;
    public float orgSizeY;
    void Awake()
    {
        orgSizeY = box1.size.y;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SizePivotY(float sizeY)
    {
        if (sizeY > 0) {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            Math.Abs(sizeY);
        } else {
            this.transform.eulerAngles = new Vector3(0, 0, 180);
            sizeY *= -1;
        }
        box1.size = new Vector3(box1.size.x, sizeY, box1.size.z);
        box2.size = new Vector3(box2.size.x, sizeY, box2.size.z);
        box3.size = new Vector3(box3.size.x, sizeY, box3.size.z);
        box4.size = new Vector3(box4.size.x, sizeY, box4.size.z);
        box1.center = new Vector3(box1.center.x, sizeY/2, box1.center.z);
        box2.center = new Vector3(box2.center.x, sizeY/2, box2.center.z);
        box3.center = new Vector3(box3.center.x, sizeY/2, box3.center.z);
        box4.center = new Vector3(box4.center.x, sizeY/2, box4.center.z);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(WaterBoxCollider))]
public class BoxColliderEdit : Editor {
    SerializedProperty newSizeY;
    private void OnEnable()
    {
        newSizeY = serializedObject.FindProperty("orgSizeY");
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();
        WaterBoxCollider boxCollider = (WaterBoxCollider)target;

        EditorGUILayout.LabelField("Size On Pivot");
        EditorGUILayout.PropertyField(newSizeY, new GUIContent("Y"));
        float sizeY = newSizeY.floatValue;
        boxCollider.SizePivotY(-sizeY);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
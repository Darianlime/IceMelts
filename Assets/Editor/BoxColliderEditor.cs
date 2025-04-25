using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoxCollider))]
public class BoxColliderEditor : Editor {
    public float sizeY;
    float originalY;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        BoxCollider boxCollider = (BoxCollider)target;

        EditorGUILayout.LabelField("Size On Pivot");
        sizeY = EditorGUILayout.FloatField("Y", sizeY);
        if (sizeY > originalY) {
            boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y + sizeY,  boxCollider.size.z);
        } 
        if (sizeY < originalY) {
            boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y - sizeY,  boxCollider.size.z);
        }
        originalY = sizeY;
    }

}



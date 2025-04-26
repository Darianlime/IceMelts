using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class RayMarch : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 positionShader;
    public MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        positionShader = this.transform.position;
    }

    public void SetShaderPosition(Vector3 position) {
        meshRenderer.sharedMaterial.SetVector("_POSITION_OFFSET", position);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RayMarch))]
public class RayMarchEditor : Editor {
    
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();
        RayMarch rayMarch = (RayMarch)target;
        rayMarch.SetShaderPosition(rayMarch.gameObject.transform.position);
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
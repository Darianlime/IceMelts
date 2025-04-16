using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.ProBuilder.MeshOperations;

public class CreateChainEditor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chain;
    public GameObject parent;
    public BoxCollider chainCollider;
    public int chainLength = 0;
    public int chainSize = 0;
    public List<GameObject> chainsList;

    void Start()
    {
        chainCollider = GetComponent<BoxCollider>();
    }

    public void InstantiateChain() {
        float chainLengthPos = 0;
        for (int i = 0; i < chainLength; i++) {
            GameObject chainObject = Instantiate(chain, parent.transform.position + new Vector3(chainLengthPos, 0, 0), new Quaternion(-180, 0, -180, 0), parent.transform);
            chainsList.Add(chainObject);
            chainLengthPos += 0.75f;
        }
        CombineMeshes(this.gameObject);
        DestoryChains();
        float chainSizePos = 0;
        for (int i = 0; i < chainSize; i++) {
            GameObject chainObject = Instantiate(parent, parent.transform.position + new Vector3(0, 0, chainSizePos), new Quaternion(0, 0, 0, 0), parent.transform);
            chainsList.Add(chainObject);
            chainSizePos += 1f;
        }
        CombineMeshes(this.gameObject);
        UpdateCollider(chainLengthPos, chainSizePos);
        DestoryChains();
    }

    public void DestoryChains() {
        foreach (GameObject chain in chainsList) {
            DestroyImmediate(chain);
        }
        chainsList = new List<GameObject>();
    }

    public void UpdateCollider(float chainLength, float chainSize) {
        chainCollider.size = new Vector3(1 + chainLength - 0.75f, 0.5f, 1 + chainSize - 1);
        chainCollider.center = new Vector3((chainLength/2) - 0.375f, 0, (chainSize/2) - 0.5f);
    }

    public void CombineMeshes(GameObject obj)
    {
        Vector3 position = obj.transform.position;
        MeshFilter[] meshFilters = new MeshFilter[chainsList.Count];
        obj.transform.position = Vector3.zero;
        int i = 0;
        for (i = 0; i < chainsList.Count; i++) {
            meshFilters[i] = chainsList[i].GetComponent<MeshFilter>();
        }
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            i++;
        }
        obj.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        obj.transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine, true, true);
        obj.transform.position = position;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CreateChainEditor))]
public class MakeChainEditor : Editor {
    SerializedProperty newSizeY;
    private void OnEnable()
    {
        newSizeY = serializedObject.FindProperty("orgSizeY");
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();
        CreateChainEditor chainEditor = (CreateChainEditor)target;

        if (GUILayout.Button("Create Chain")) {
            chainEditor.InstantiateChain();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
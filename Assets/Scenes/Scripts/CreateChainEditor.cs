using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.ProBuilder.MeshOperations;
using System.Drawing;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class CreateChainEditor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chain;
    public GameObject chainPhysics;
    public BoxCollider chainCollider;
    public int chainLength = 0;
    public int chainSize = 0;
    public List<GameObject> chainsList;

    void Start()
    {
        chainCollider = GetComponent<BoxCollider>();
    }

    public void ChainStatic(bool isVertical) {
        chainCollider.enabled = true;
        DestoryChains();
        float chainLengthPos = InstantiateChain(chain, false, this.gameObject);
        CombineMeshes(this.gameObject);
        DestoryChains();

        float chainSizePos = InstantiateChain(chain, true, this.gameObject);
        CombineMeshes(this.gameObject);

        UpdateCollider(chainLengthPos, chainSizePos);
        DestoryChains();
        this.transform.rotation = isVertical ? Quaternion.Euler(0, -90, -90) : Quaternion.Euler(0, 0, 0);
    }

    public void ChainPhysics(bool isVertical) {
        chainCollider.center = Vector3.zero;
        chainCollider.size = Vector3.zero;
        chainCollider.enabled = false;
        DestoryChains();
        GameObject storeChains = new GameObject("ChainsLinked");
        GameObject makeStoreChain = Instantiate(storeChains, this.transform);

        GameObject startChain = new GameObject("Anchor Chain");
        startChain.AddComponent<Rigidbody>();
        Rigidbody startChainRb = startChain.GetComponent<Rigidbody>();
        startChainRb.isKinematic = true;

        this.GetComponent<MeshFilter>().sharedMesh.Clear();

        GameObject chainObject;
        chainObject = Instantiate(startChain, this.transform.position + new Vector3(-0.6f, 0, 0), new Quaternion(-180, 0, -180, 0), makeStoreChain.transform);
        chainsList.Add(chainObject);

        float chainLength = InstantiateChain(chainPhysics, false, makeStoreChain);

        chainObject = Instantiate(startChain, this.transform.position + new Vector3(chainLength - 0.75f + 0.6f, 0, 0), new Quaternion(-180, 0, -180, 0), makeStoreChain.transform);
        chainsList.Add(chainObject);
        SetHingePoints();
        ExcludeLayers();

        chainsList = new List<GameObject>();

        InstantiateChain(null, true, makeStoreChain);

        DestroyImmediate(startChain);
        DestroyImmediate(storeChains);
        DestroyImmediate(makeStoreChain);
        this.transform.rotation = isVertical ? Quaternion.Euler(0, -90, -90) : Quaternion.Euler(0, 0, 0);
    }

    public float InstantiateChain(GameObject chain, bool isSize, GameObject isStatic) {
        GameObject chainObject;
        float chainLengthPos = 0;
        int transformLength = isSize ? chainSize : chainLength;
        for (int i = 0; i < transformLength; i++) {
            if (isSize) {
                chainObject = Instantiate(isStatic, this.transform.position + new Vector3(0, 0, chainLengthPos), new Quaternion(0, 0, 0, 0), this.transform);
                chainLengthPos += 1f;
            } else {
                chainObject = Instantiate(chain, this.transform.position + new Vector3(chainLengthPos, 0, 0), Quaternion.Euler(0, 90, 0), isStatic.transform);
                chainLengthPos += 0.75f;
            }
            chainsList.Add(chainObject);
        }
        return chainLengthPos;
    }

    public void ExcludeLayers() {
        for (int i = 1; i < chainsList.Count - 2; i++) {
            CapsuleCollider collider = chainsList[i].GetComponent<CapsuleCollider>();
            collider.excludeLayers = LayerMask.GetMask("Water");
        }
    }

    public void SetHingePoints() {
        HingeJoint hinge;
        int lastChain = chainsList.Count-2;
        for (int i = 1; i <= lastChain; i++) {
            hinge = chainsList[i].GetComponent<HingeJoint>();
            chainsList[i].GetComponent<HingeJoint>().axis = new Vector3(0,0,1);
            hinge.connectedBody = chainsList[i - 1].GetComponent<Rigidbody>();
        }
        chainsList[1].GetComponent<HingeJoint>().axis = new Vector3(0,1,0);
        chainsList[lastChain].AddComponent<HingeJoint>();
        hinge = (HingeJoint)chainsList[lastChain].GetComponentAtIndex(chainsList[lastChain].GetComponentCount()-1);
        hinge.connectedBody = chainsList[lastChain + 1].GetComponent<Rigidbody>();
        hinge.anchor = new Vector3(0, 0, 0.5f);
        hinge.axis = new Vector3(0, 1, 0);
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
    bool toggle = false;
    bool toggleVertical = false;
    string staticChain = "Create Static Chain";
    string physcisChain = "Create Chain With Physics";

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();
        CreateChainEditor chainEditor = (CreateChainEditor)target;
        toggle = EditorGUILayout.Toggle("Physics On", toggle);
        string chain = toggle ? physcisChain : staticChain;
        toggleVertical = EditorGUILayout.Toggle("Vertical?", toggleVertical);
        chain = toggleVertical ? chain + " Vertical" : chain + " Horizontal";
        if (GUILayout.Button(chain)) {
            if (toggle) {
                chainEditor.ChainPhysics(toggleVertical);
            } else {
                chainEditor.ChainStatic(toggleVertical);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
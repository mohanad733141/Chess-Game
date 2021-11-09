using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh_renderer;
    private MeshRenderer meshRenderer {
        get {
            if(meshRenderer == null) {
                mesh_renderer = GetComponent<MeshRenderer>();
                return mesh_renderer;
            }
        }
    }

    public void setMat(Material m) {
        mesh_renderer.material = m;
    }
}

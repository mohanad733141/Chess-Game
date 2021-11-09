using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh_renderComponent;
    private MeshRenderer meshRenderComponent {
        get {
            if(meshRenderComponent == null) 
                mesh_renderComponent = GetComponent<MeshRenderer>();
                return mesh_renderComponent;
            
        }
    }

    public void setMat(Material m) {
        mesh_renderComponent.material = m;
    }
}

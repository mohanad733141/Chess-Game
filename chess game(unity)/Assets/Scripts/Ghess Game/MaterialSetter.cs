using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class MaterialSetter : MonoBehaviour
{
    private MeshRenderer mesh_renderComp;

    private MeshRenderer meshRenComp
    {
        get
        {
            if (mesh_renderComp == null)
            {
                mesh_renderComp = GetComponent<MeshRenderer>();
            }
            return mesh_renderComp;

        }
    }

    public void setMat(Material m)
    {
        meshRenComp.material = m;
    }
}

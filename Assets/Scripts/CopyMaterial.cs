using System.Collections.Generic;
using UnityEngine;

public class CopyMaterial : MonoBehaviour
{
    private Material materialCopy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        materialCopy = new Material(GetComponentInChildren<MeshRenderer>().material);
        List<Material> materials = new()
        {
            materialCopy
        };
        GetComponentInChildren<MeshRenderer>().SetMaterials(materials);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

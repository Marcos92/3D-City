using System;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private MeshRenderer mesh;
    [SerializeField] private Material validMaterial;
    [SerializeField] private Material invalidMaterial;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void SetValid(bool value)
    {
        mesh.material = value ? validMaterial : invalidMaterial;
    }
}
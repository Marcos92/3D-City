using UnityEngine;

[CreateAssetMenu(fileName = "BuildingInfo", menuName = "ScriptableObjects/BuildingInfo", order = 1)]
public class BuildingInfo : ScriptableObject
{
    public GameObject model;
    public Vector3 size;
}
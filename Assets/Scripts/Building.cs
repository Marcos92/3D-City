using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{
    public BuildingInfo info;
    private Vector3 size;

    void Awake()
    {
        size = info.size;
        GetComponent<BoxCollider>().size = size;
    }

    void OnEnable()
    {
        Instantiate(info.model, transform.position, Quaternion.identity, transform);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(transform.position, size);
    }
}
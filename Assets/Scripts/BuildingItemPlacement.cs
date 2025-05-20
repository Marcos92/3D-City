using UnityEngine;

public class BuildingItemPlacement : MonoBehaviour
{
    private LayerMask planeMask;
    private LayerMask obstacleMask;

    private RaycastHit hit;

    public bool isSelected;

    private bool isValid;
    public bool IsValid => isValid;

    public Vector3 size;

    void Awake()
    {
        planeMask = LayerMask.GetMask("Plane");
        obstacleMask = LayerMask.GetMask("Obstacle");
    }

    void FixedUpdate()
    {
        if (!isSelected)
        {
            isValid = false;
            return;
        }

        isValid = CanPlaceOnPlane() && !IsColliding();
    }

    private bool CanPlaceOnPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, planeMask);
    }

    private bool IsColliding()
    {
        Collider[] colliders = Physics.OverlapBox(hit.point, size * 0.5f, Quaternion.identity, obstacleMask);
        return colliders.Length > 0;
    }

    void OnDrawGizmos()
    {
        if (!isSelected)
        {
            return;
        }

        if (CanPlaceOnPlane())
        {
            Gizmos.color = IsColliding() ? Color.red : Color.green;
            Gizmos.DrawCube(hit.point, size);
        }
    }
}
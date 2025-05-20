using UnityEngine;

public class BuildingPlacementRaycast : MonoBehaviour
{
    private LayerMask layerMask;
    private RaycastHit hit;

    private bool isOnPlane;
    private bool isValid;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Plane");
    }

    void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        isOnPlane = Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
    }

    void OnDrawGizmos()
    {
        if (isOnPlane)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hit.point, 5.0f);
        }
    }
}